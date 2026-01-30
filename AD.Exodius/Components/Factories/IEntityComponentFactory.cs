using AD.Exodius.Components;

namespace AD.Exodius.Components.Factories;

/// <summary>
/// Factory interface for creating entity components.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IEntityComponentFactory
{
    /// <summary>
    /// Creates an instance of the specified entity component type.
    /// </summary>
    /// <typeparam name="TEntityComponent">The type of the entity component to create. Must implement <see cref="IEntityComponent"/>.</typeparam>
    /// <returns>An instance of the specified entity component type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified component type does not exist in the factory.</exception>
    TEntityComponent Create<TEntityComponent>() where TEntityComponent : IEntityComponent;
}
