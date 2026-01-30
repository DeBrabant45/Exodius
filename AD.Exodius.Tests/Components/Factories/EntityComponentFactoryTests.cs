using AD.Exodius.Components.Factories;
using AD.Exodius.Drivers;
using AD.Exodius.Tests.Stubs.Components;
using NSubstitute;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Components.Factories;

public class EntityComponentFactoryTests
{
    private readonly IDriver _mockDriver;
    private readonly IEventBus _mockEventBus;
    private readonly IPageEntity _mockPageObject;


    public EntityComponentFactoryTests()
    {
        _mockDriver = Substitute.For<IDriver>();
        _mockEventBus = Substitute.For<IEventBus>();    
        _mockPageObject = Substitute.For<IPageEntity>();
    }

    [Fact]
    public void Create_ShouldThrowArgumentNullException_WhenOwnerIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            EntityComponentFactory.Create<StubBasicComponent>(_mockDriver, null!, _mockEventBus));

        Assert.Equal("Value cannot be null. (Parameter 'owner')", exception.Message);
    }

    [Fact]
    public void Create_ShouldThrowMissingMethodException_WhenComponentCannotBeCreated()
    {
        var exception = Assert.Throws<MissingMethodException>(() =>
            EntityComponentFactory.Create<NonCreatableComponent>(_mockDriver, _mockPageObject, _mockEventBus));

        Assert.Equal($"Constructor on type '{typeof(NonCreatableComponent).FullName}' not found.", exception.Message);
    }

    [Fact]
    public void Create_ShouldReturnExpectedComponent()
    {
        var result = EntityComponentFactory.Create<StubBasicComponent>(_mockDriver, _mockPageObject, _mockEventBus);

        Assert.NotNull(result);
        Assert.IsType<StubBasicComponent>(result);
    }

    [Fact]
    public void Create_ByType_ShouldThrowArgumentNullException_WhenTypeIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            EntityComponentFactory.Create(null!, _mockDriver, _mockPageObject, _mockEventBus));

        Assert.Equal("Value cannot be null. (Parameter 'type')", exception.Message);
    }

    [Fact]
    public void Create_ByType_ShouldThrowArgumentNullException_WhenDriverIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            EntityComponentFactory.Create(typeof(StubBasicComponent), null!, _mockPageObject, _mockEventBus));

        Assert.Equal("Value cannot be null. (Parameter 'driver')", exception.Message);
    }

    [Fact]
    public void Create_ByType_ShouldThrowArgumentNullException_WhenOwnerIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            EntityComponentFactory.Create(typeof(StubBasicComponent), _mockDriver, null!, _mockEventBus));

        Assert.Equal("Value cannot be null. (Parameter 'owner')", exception.Message);
    }

    [Fact]
    public void Create_ByType_ShouldThrowInvalidOperationException_WhenCannotInstantiate()
    {
        var exception = Assert.Throws<InvalidOperationException>(() =>
            EntityComponentFactory.Create(typeof(NonCreatableComponent), _mockDriver, _mockPageObject, _mockEventBus));

        Assert.Equal($"Constructor with parameters (IDriver, IEntity, IEventBus) not found on type '{typeof(NonCreatableComponent).FullName}'.", exception.Message);
    }

    [Fact]
    public void Create_ByType_ShouldReturnExpectedComponent()
    {
        var result = EntityComponentFactory.Create(typeof(StubBasicComponent), _mockDriver, _mockPageObject, _mockEventBus);

        Assert.NotNull(result);
        Assert.IsType<StubBasicComponent>(result);
    }
}
