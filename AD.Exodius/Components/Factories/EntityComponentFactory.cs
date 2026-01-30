using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Components.Factories;

public static class EntityComponentFactory 
{
    public static TEntityComponent Create<TEntityComponent>(IDriver driver, IEntity owner, IEventBus eventBus) where TEntityComponent : IEntityComponent
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(driver);

        var instance = Activator.CreateInstance(typeof(TEntityComponent), driver, owner, eventBus)
            ?? throw new InvalidOperationException($"Failed to create an instance of {typeof(TEntityComponent).Name}.");

        return (TEntityComponent)instance;
    }

    public static IEntityComponent Create(Type type, IDriver driver, IEntity owner, IEventBus eventBus)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(driver);
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(eventBus);

        if (!typeof(IEntityComponent).IsAssignableFrom(type))
            throw new InvalidOperationException($"{type.FullName} does not implement IEntityComponent.");

        var ctor = type.GetConstructor([typeof(IDriver), typeof(IEntity), typeof(IEventBus)]) 
            ?? throw new InvalidOperationException($"Constructor with parameters (IDriver, IEntity, IEventBus) not found on type '{type.FullName}'.");

        var instance = (IEntityComponent?)ctor.Invoke([driver, owner, eventBus]) 
            ?? throw new InvalidOperationException($"Failed to create an instance of {type.FullName}.");

        return instance;
    }
}
