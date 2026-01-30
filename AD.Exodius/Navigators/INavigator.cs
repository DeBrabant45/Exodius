using AD.Exodius.Entities.Pages;
using AD.Exodius.Navigators.Strategies;

namespace AD.Exodius.Navigators;

/// <summary>
/// Represents a navigator responsible for transitioning between web pages.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface INavigator
{
    /// <summary>
    /// Navigates to a specified web page using a defined navigation strategy.
    /// </summary>
    /// <typeparam name="TPage">The type of the web page to navigate to.</typeparam>
    /// <typeparam name="TNavigation">The type of the navigation strategy to be used.</typeparam>
    /// <returns>A task representing the asynchronous operation, with a result of the specified page type <typeparamref name="TPage"/>.</returns>
    public Task<TPage> GoToAsync<TPage, TNavigation>() where TPage : IPageEntity where TNavigation : INavigationStrategy;
}
