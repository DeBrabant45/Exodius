namespace AD.Exodius.Components;

/// <summary>
/// Represents a lazily initialized component within an entity's component graph.
/// Used for components that require deferred setup after initial registration.
/// </summary>
/// <remarks>
/// Implementing this interface allows the entity to call <see cref="Initialize"/> 
/// explicitly during a post-assembly phase, enabling on-demand configuration of dependencies, 
/// data bindings, or browser interactions.
/// </remarks>
/// <author>Aaron DeBrabant</author>
public interface ILazyEntityComponent : IEntityComponent
{
    /// <summary>
    /// Performs any deferred initialization required by the component.
    /// Called after the component has been registered and constructed, 
    /// typically as part of the entity’s <c>InitializeLazyComponents</c> phase.
    /// </summary>
    void Initialize();
}
