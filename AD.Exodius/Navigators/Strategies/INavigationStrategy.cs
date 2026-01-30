using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;

namespace AD.Exodius.Navigators.Strategies;

/// <summary>
/// Defines a strategy for navigating to a specific page.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface INavigationStrategy
{
    /// <summary>
    /// Navigates to the specified page using the provided driver.
    /// </summary>
    /// <typeparam name="TPage">The type of the page to navigate to, which must implement <see cref="IPageEntity"/>.</typeparam>
    /// <param name="driver">The driver used to perform the navigation.</param>
    /// <param name="page">The page to navigate to.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    public Task Navigate<TPage>(IDriver driver, TPage page) where TPage : IPageEntity;
}
