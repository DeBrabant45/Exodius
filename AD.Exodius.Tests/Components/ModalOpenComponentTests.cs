using NSubstitute;
using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Components;
using AD.Exodius.Tests.Stubs.Entities.Modals;

namespace AD.Exodius.Tests.Components;

public class ModalOpenComponentTests
{
    private readonly IDriver _driver;
    private readonly IPageEntity _owner;
    private readonly IEventBus _eventBus;

    public ModalOpenComponentTests()
    {
        _driver = Substitute.For<IDriver>();
        _owner = Substitute.For<IPageEntity>();
        _eventBus = Substitute.For<EventBus>();
    }

    [Fact]
    public async Task OpenModal_Should_CallTriggerOpenAndInitializeModal_When_Invoked()
    {
        var opener = new TestModalOpener(_driver, _owner, _eventBus);

        var modal = await opener.OpenModal<StubSampleModal>();

        Assert.NotNull(modal);
        Assert.True(opener.TriggerOpenCalled);
        Assert.True(modal.WasInitialized);
    }

    [Fact]
    public async Task OpenModal_Should_ReturnModalInstanceOfCorrectType_When_ValidModalRequested()
    {
        var opener = new TestModalOpener(_driver, _owner, _eventBus);

        var modal = await opener.OpenModal<StubSampleModal>();

        Assert.IsType<StubSampleModal>(modal);
    }
}
