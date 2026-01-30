using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Components;

public class EntityComponent : IEntityComponent
{
    protected readonly IDriver Driver;
    protected readonly IEntity Owner;
    protected readonly IEventBus EventBus;

    public EntityComponent(IDriver driver, IEntity owner, IEventBus eventBus)
    {
        Driver = driver;
        Owner = owner;
        EventBus = eventBus;
    }
}
