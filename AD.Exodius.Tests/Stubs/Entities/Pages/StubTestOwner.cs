using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;
using AD.Exodius.Tests.Stubs.Attributes;

namespace AD.Exodius.Tests.Stubs.Entities.Pages;

public interface ITestGraph : IEntity { }

[Mock("TestOwner")]
public class StubTestOwner : PageEntity, ITestGraph
{
    public StubTestOwner(IDriver driver, IEventBus eventBus)
        : base(driver, eventBus)
    {

    }
}