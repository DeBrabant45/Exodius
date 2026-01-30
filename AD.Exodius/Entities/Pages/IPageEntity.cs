namespace AD.Exodius.Entities.Pages;

/// <summary>
/// Represents a page entity within the component graph architecture. 
/// A page entity orchestrates behavior through its registered components 
/// and provides lifecycle readiness capabilities.
/// </summary>
/// <remarks>
/// Inherits from <see cref="IEntity"/>, giving it access to component 
/// registration, assembly, and resolution mechanisms.
/// </remarks>
/// <author>Aaron DeBrabant</author>
public interface IPageEntity : IEntity
{
    /// <summary>
    /// Asynchronously waits for the page and its critical components to become ready for interaction.
    /// This ensures the entity is fully initialized before use in test workflows or UI automation logic.
    /// </summary>
    /// <returns>A task representing the asynchronous wait operation.</returns>
    Task WaitUntilReady();
}
