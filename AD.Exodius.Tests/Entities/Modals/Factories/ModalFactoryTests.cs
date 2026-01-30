using NSubstitute;
using AD.Exodius.Drivers;
using AD.Exodius.Entities.Modals.Factories;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Entities.Modals;

namespace AD.Exodius.Tests.Entities.Modals.Factories;

public class ModalFactoryTests
{
    private readonly IDriver _driver;
    private readonly IPageEntity _owner;
    private readonly IEventBus _eventBus;

    public ModalFactoryTests()
    {
        _driver = Substitute.For<IDriver>();
        _owner = Substitute.For<IPageEntity>();
        _eventBus = Substitute.For<EventBus>();
    }

    [Fact]
    public void Create_ShouldReturnModalInstance_WhenArgumentsAreValid()
    {
        var modal = ModalFactory.Create<StubSampleModal>(_driver, _eventBus, _owner);

        Assert.NotNull(modal);
        Assert.IsType<StubSampleModal>(modal);
        Assert.Same(_driver, modal.GetTestDriver());
        Assert.Same(_owner, modal.GetTestOwner());
    }

    [Fact]
    public void Create_ShouldThrowArgumentNullException_WhenDriverIsNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ModalFactory.Create<StubSampleModal>(null!, _eventBus, _owner));

        Assert.Equal("driver", ex.ParamName);
    }

    [Fact]
    public void Create_ShouldThrowArgumentNullException_WhenOwnerIsNull()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ModalFactory.Create<StubSampleModal>(_driver, _eventBus, null!));

        Assert.Equal("owner", ex.ParamName);
    }

    [Fact]
    public void Create_ShouldThrowMissingMethodException_WhenConstructorIsMissing()
    {
        var ex = Assert.Throws<MissingMethodException>(() =>
            ModalFactory.Create<StubInvalidModal>(_driver, _eventBus, _owner));

        Assert.Contains(nameof(StubInvalidModal), ex.Message);
    }
}
