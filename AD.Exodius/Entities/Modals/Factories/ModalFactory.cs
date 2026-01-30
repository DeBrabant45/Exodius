using AD.Exodius.Drivers;
using AD.Exodius.Events;

namespace AD.Exodius.Entities.Modals.Factories;

public static class ModalFactory
{
    public static TModalEntity Create<TModalEntity>(IDriver driver, IEventBus eventBus, IEntity owner) where TModalEntity : IModalEntity
    {
        ArgumentNullException.ThrowIfNull(driver);
        ArgumentNullException.ThrowIfNull(owner);

        var instance = Activator.CreateInstance(typeof(TModalEntity), driver, eventBus, owner)
            ?? throw new InvalidOperationException($"Failed to create an instance of {typeof(TModalEntity).Name}.");

        return (TModalEntity)instance;
    }
}
