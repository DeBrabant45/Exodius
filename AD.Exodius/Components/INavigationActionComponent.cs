using AD.Exodius.Components;

namespace AD.Exodius.Components;

/// <summary>
/// Represents a component capable of performing navigation actions.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface INavigationActionComponent : IEntityComponent
{
    /// <summary>
    /// Performs a click action on the specified navigation item.
    /// </summary>
    /// <param name="item">The identifier of the navigation item to click.</param>
    /// <returns>
    /// A task that represents the asynchronous operation of performing the click action.
    /// </returns>
    public Task ClickAction(string item);
}
