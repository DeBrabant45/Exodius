using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Components;

public class TestModalOpener : ModalOpenComponent
{
    public bool TriggerOpenCalled { get; private set; }

    public TestModalOpener(IDriver driver, IEntity owner, IEventBus eventBus)
        : base(driver, owner, eventBus)
    {

    }

    protected override Task TriggerOpen()
    {
        TriggerOpenCalled = true;
        return Task.CompletedTask;
    }
}
