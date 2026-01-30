using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.PageObjects;

[PageEntityMeta(
    Route = "/home",
    Name = "Example",
    DomId = "test"
)]
public class StubMetaPageEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}
