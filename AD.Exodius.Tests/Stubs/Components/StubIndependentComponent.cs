using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Components;

public class StubIndependentComponent : EntityComponent
{
    public StubIndependentComponent(IDriver driver, IEntity owner, IEventBus eventBus) : base(driver, owner, eventBus) { }
}
