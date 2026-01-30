using NSubstitute;
using System.Reflection;
using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Components;

namespace AD.Exodius.Tests.Entities;

public class EntityTests
{
    private readonly Entity _sut;

    public EntityTests()
    {
        var mockedDriver = Substitute.For<IDriver>();
        var mockedEventBus = Substitute.For<IEventBus>();
        _sut = new Entity(mockedDriver, mockedEventBus);
    }

    [Fact]
    public void AddComponent_Should_StoreComponent()
    {
        _sut.AddComponent<StubBasicComponent>();

        var field = typeof(Entity)
            .GetField("_unregisteredComponentTypes", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(field);

        var unregistered = (HashSet<Type>)field.GetValue(_sut)!;
        Assert.Contains(typeof(StubBasicComponent), unregistered.ToList());
    }

    [Fact]
    public void GetComponent_ShouldReturn_ExpectedResults()
    {
        _sut.AddComponent<StubBasicComponent>();
        _sut.AssembleGraph();

        var result = _sut.GetComponent<StubBasicComponent>();

        Assert.NotNull(result);
        Assert.IsType<StubBasicComponent>(result);
    }

    [Fact]
    public void GetComponents_ShouldReturn_ExpectedResults()
    {
        _sut.AddComponent<StubBasicComponent>();
        _sut.AssembleGraph();

        var results = _sut.GetComponents<StubBasicComponent>();

        Assert.Single(results);
        Assert.IsType<StubBasicComponent>(results[0]);
    }

    [Fact]
    public void RemoveComponents_Should_RemoveSpecifiedComponent()
    {
        _sut.AddComponent<StubBasicComponent>();
        _sut.AssembleGraph();

        var component = _sut.GetComponent<StubBasicComponent>();

        _sut.RemoveComponent<StubBasicComponent>();

        var results = _sut.GetComponents<StubBasicComponent>();

        Assert.Empty(results);
        Assert.DoesNotContain(component, results);
    }


    [Fact]
    public void LazyInitialize_Should_InitializeLazyComponents()
    {
        _sut.AddComponent<StubLazyPageComponent>();
        _sut.AssembleGraph();
        _sut.InitializeLazyComponents();

        var mockLazyComponent1 = _sut.GetComponent<StubLazyPageComponent>();
        var mockLazyComponent2 = _sut.GetComponent<StubLazyPageComponent>();

        Assert.True(mockLazyComponent1.IsInitialized);
        Assert.True(mockLazyComponent2.IsInitialized);
    }

    [Fact]
    public void LazyInitialize_ShouldNotThrowException_WhenNoLazyComponentsExist()
    {
        var stubPageObject = Substitute.For<IPageEntity>();
        stubPageObject.GetComponents<ILazyEntityComponent>().Returns(new List<ILazyEntityComponent>());

        var exception = Record.Exception(() => stubPageObject.InitializeLazyComponents());

        Assert.Null(exception);
    }

    [Fact]
    public void AssembleGraph_ShouldThrow_WhenCircularDependencyDetected()
    {
        _sut.AddComponent<StubComponentA>();
        _sut.AddComponent<StubComponentB>();
        _sut.AddComponent<StubComponentC>();

        var ex = Assert.Throws<InvalidOperationException>(() => _sut.AssembleGraph());

        Assert.Contains("Cyclic dependency detected:", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AssembleGraph_ShouldInitializeComponents_WithAndWithoutRequires()
    {
        _sut.AddComponent<StubIndependentComponent>();
        _sut.AddComponent<StubDependentComponent>();

        _sut.AssembleGraph();

        Assert.NotNull(_sut.GetComponent<StubIndependentComponent>());
        Assert.NotNull(_sut.GetComponent<StubDependentComponent>());
    }

    [Fact]
    public void AssembleGraph_ShouldThrow_WhenRequiredComponentIsMissing()
    {
        _sut.AddComponent<StubDependentComponent>();

        var ex = Assert.Throws<InvalidOperationException>(() => _sut.AssembleGraph());

        Assert.Contains(nameof(StubIndependentComponent), ex.Message);
    }

    [Fact]
    public void AssembleGraph_ShouldInitializeComponents_WhenInterfaceIsRequired()
    {
        _sut.AddComponent<StubInterfaceImplementation>();
        _sut.AddComponent<StubInterfaceDependentComponent>();

        _sut.AssembleGraph();

        var result = _sut.GetComponent<IStubInterface>();
        Assert.NotNull(result);
    }
}
