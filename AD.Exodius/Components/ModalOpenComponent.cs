using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Entities.Modals;
using AD.Exodius.Entities.Modals.Factories;
using AD.Exodius.Events;

namespace AD.Exodius.Components;

public abstract class ModalOpenComponent : EntityComponent, IModalOpenComponent
{
    public ModalOpenComponent(IDriver driver, IEntity owner, IEventBus eventBus) 
        : base(driver, owner, eventBus)
    {

    }

    public virtual async Task<TModalEntity> OpenModal<TModalEntity>() where TModalEntity : IModalEntity
    {
        await TriggerOpen();

        var modal = ModalFactory.Create<TModalEntity>(Driver, EventBus, Owner);
        modal.AssembleGraph();
        modal.InitializeLazyComponents();

        return modal;
    }

    protected abstract Task TriggerOpen();
}
