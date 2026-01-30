using AD.Exodius.Drivers;
using AD.Exodius.Events;

namespace AD.Exodius.Entities.Modals;

public class ModalEntity : Entity, IModalEntity
{
    protected readonly IEntity Owner;

    public ModalEntity(IDriver driver, IEventBus eventBus, IEntity owner) 
        : base(driver, eventBus)
    {
        Owner = owner;
    }
}
