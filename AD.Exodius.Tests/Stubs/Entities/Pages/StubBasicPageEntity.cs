using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.Pages;

public class StubBasicPageEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}
