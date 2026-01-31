using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.Pages;

[PageEntityMeta(
    Route = "/home",
    Name = "Example",
    DomId = "test",
    QueryString = "?param1=value1&param2=value2"
)]
public class StubMetaPageEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}
