using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.Pages;

[PageEntitytName("Sample Attribute")]
public class StubSampleAttributeEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}