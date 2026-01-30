using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Components;

public class StubLazyPageComponent(IDriver driver, IEntity owner, IEventBus eventBus) : LazyEntityComponent(driver, owner, eventBus)
{
    public bool IsInitialized { get; private set; }

    public override void Initialize()
    {
        IsInitialized = true;
    }
}
