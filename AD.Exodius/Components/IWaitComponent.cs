namespace AD.Exodius.Components;

/// <summary>
/// Defines a contract for waiting until a section or page has fully loaded.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IWaitComponent : IEntityComponent
{
    /// <summary>
    /// Waits until the section or page is fully loaded, ensuring that all required elements and content are visible and ready for interaction.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task WaitUntilFullyLoaded();
}
