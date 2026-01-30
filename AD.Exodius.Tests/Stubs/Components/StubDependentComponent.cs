using AD.Exodius.Components;
using AD.Exodius.Components.Attributes;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Components;

[Requires(typeof(StubIndependentComponent))]
public class StubDependentComponent : EntityComponent
{
    public StubDependentComponent(IDriver driver, IEntity owner, IEventBus eventBus) : base(driver, owner, eventBus)
    {
        owner.GetComponent<StubIndependentComponent>();
    }
}
