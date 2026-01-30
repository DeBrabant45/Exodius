using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.PageObjects;

[PageEntitytName("Sample Attribute")]
public class StubSampleAttributeEntity(IDriver driver, IEventBus eventBus) : PageEntity(driver, eventBus)
{

}