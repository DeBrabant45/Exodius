using NSubstitute;
using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Components;
using AD.Exodius.Tests.Stubs.Entities.PageObjects;

namespace AD.Exodius.Tests.Components;

public class EntityComponentTests
{
    private readonly IDriver _driver;
    private readonly IEventBus _eventBus;

    public EntityComponentTests()
    {
        _driver = Substitute.For<IDriver>();
        _eventBus = Substitute.For<IEventBus>();
    }

    [Fact]
    public void GetOwner_ShouldReturnsOwner_WhenOwnerIsOfExpectedType()
    {
        var owner = new StubTestOwner(_driver, _eventBus);
        var component = new TestPageComponent(_driver, owner, _eventBus);

        var result = component.CallGetOwner<ITestGraph>();

        Assert.Equal(owner, result);
    }

    [Fact]
    public void GetMockAttributeLabel_ShouldReturnsLabelFromOwnerAttribute()
    {
        var owner = new StubTestOwner(_driver, _eventBus);
        var component = new AttributeAwareComponent(_driver, owner, _eventBus);

        var label = component.GetMockLabelFromOwner();

        Assert.Equal("TestOwner", label);
    }

    [Fact]
    public void GetOwner_ShouldThrowException_WhenOwnerIsNotExpectedType()
    {
        var wrongOwner = Substitute.For<IEntity>();
        var component = new TestPageComponent(_driver, wrongOwner, _eventBus);

        var ex = Assert.Throws<InvalidCastException>(() =>
            component.CallGetOwner<ITestGraph>());

        Assert.Contains("Unable to cast object of type", ex.Message);
    }

    internal class TestPageComponent : EntityComponent
    {
        public TestPageComponent(IDriver driver, IEntity owner, IEventBus eventBus)
            : base(driver, owner, eventBus) { }

        public TOwner CallGetOwner<TOwner>() where TOwner : class, IEntity
            => (TOwner)Owner;
    }
}