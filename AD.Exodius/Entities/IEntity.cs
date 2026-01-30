using AD.Exodius.Components;

namespace AD.Exodius.Entities;

/// <summary>
/// Represents an entity-based component graph responsible for managing and assembling interconnected UI components.
/// Entities own and orchestrate reusable page or modal components, resolving dependencies and behaviors dynamically.
/// </summary>
/// /// <author>Aaron DeBrabant</author>
public interface IEntity
{
    /// <summary>
    /// Registers a component type to the graph. Components must implement <see cref="IEntityComponent"/>.
    /// Registration prepares the component for resolution via dependency injection or internal factory mechanisms.
    /// </summary>
    /// <typeparam name="TPageComponent">The type of the component to register.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if the component type is already registered or invalid.</exception>
    void AddComponent<TPageComponent>() where TPageComponent : IEntityComponent;

    /// <summary>
    /// Assembles the component graph by resolving and connecting all registered components and their declared dependencies.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if cyclic dependencies exist or required components are missing.</exception>
    void AssembleGraph();

    /// <summary>
    /// Retrieves a single instance of a component from the assembled graph.
    /// </summary>
    /// <typeparam name="TPageComponent">The type of the component to retrieve.</typeparam>
    /// <returns>The resolved component instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the component has not been assembled in the graph.</exception>
    TPageComponent GetComponent<TPageComponent>() where TPageComponent : IEntityComponent;

    /// <summary>
    /// Retrieves all components of a given type from the graph.
    /// Useful for filtering by base type or interface when multiple implementations exist.
    /// </summary>
    /// <typeparam name="TPageComponent">The base or interface type to filter components.</typeparam>
    /// <returns>A list of matching component instances.</returns>
    List<TPageComponent> GetComponents<TPageComponent>() where TPageComponent : IEntityComponent;

    /// <summary>
    /// Initializes all components that implement <see cref="ILazyEntityComponent"/>.
    /// This supports deferred logic such as DOM binding, data synchronization, or UI readiness checks.
    /// </summary>
    void InitializeLazyComponents();

    /// <summary>
    /// Removes a component of the specified type from the graph.
    /// </summary>
    /// <typeparam name="TPageComponent">The type of the component to remove.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if the component was not previously assembled.</exception>
    void RemoveComponent<TPageComponent>() where TPageComponent : IEntityComponent;
}
