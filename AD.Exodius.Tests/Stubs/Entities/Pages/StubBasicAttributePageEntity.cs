using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.Pages;

[PageEntitytName("Basic Attribute")]
[PageEntityRoute("/basic")]
public class StubBasicAttributePageEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}
