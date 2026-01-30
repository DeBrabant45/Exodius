using NSubstitute;
using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Entities.Pages;

public class PageEntityTests
{
    private readonly IDriver _driver;
    private readonly EventBus _eventBus;

    public PageEntityTests()
    {
        _driver = Substitute.For<IDriver>();
        _eventBus = new EventBus();
    }

    private class EventPageEntity : PageEntity
    {
        public bool WaitCalled { get; private set; }

        public EventPageEntity(IDriver driver, IEventBus eventBus) 
            : base(driver, eventBus) 
        { 

        }

        public override async Task WaitUntilReady()
        {
            WaitCalled = true;
            await Task.CompletedTask; 
        }
    }

    [Fact]
    public async Task WaitUntilReady_ShouldBeCalled_WhenPageReadyCheckEventIsPublished()
    {
        var page = new EventPageEntity(_driver, _eventBus);

        await _eventBus.Publish(new PageReadyCheckEvent());

        Assert.True(page.WaitCalled);
    }
}
