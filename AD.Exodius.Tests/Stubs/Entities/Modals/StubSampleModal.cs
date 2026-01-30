using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Entities.Modals;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Entities.Modals;

public class StubSampleModal : ModalEntity
{
    public StubSampleModal(IDriver driver, IEventBus eventBus, IEntity owner) 
        : base(driver, eventBus, owner)
    {
        WasInitialized = true;
    }

    public IPageEntity GetTestOwner() => (IPageEntity)Owner;

    public IDriver GetTestDriver() => Driver;

    public bool WasInitialized { get; private set; }
}
