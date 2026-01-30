using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Components;

public abstract class LazyEntityComponent : EntityComponent, ILazyEntityComponent
{
    protected LazyEntityComponent(IDriver driver, IEntity owner, IEventBus eventBus) 
        : base(driver, owner, eventBus)
    {

    }

    public abstract void Initialize();
}
