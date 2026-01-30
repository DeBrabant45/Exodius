using AD.Exodius.Drivers.Enums;
using AD.Exodius.Elements;

namespace AD.Exodius.Drivers.Services;

/// <summary>
/// Represents a service for navigating web pages.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface INavigationService
{
    /// <summary>
    /// Navigates to the specified URL and handles navigation errors based on the provided error behavior.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    /// <param name="errorBehavior">Specifies how navigation errors should be handled. The default behavior is to throw an exception for navigation errors. If set to <see cref="ErrorBehavior.LogException"/>, navigation errors (like a 500 server error) are logged, but the operation continues.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    public Task GoToUrlAsync(string url, ErrorBehavior errorBehavior = ErrorBehavior.ThrowException);

    /// <summary>
    /// Navigates to the first available URL from a collection of URLs, trying them in order until one succeeds.
    /// If the page does not redirect to a URL that matches the requested value, navigation is considered a failure for that attempt.
    /// </summary>
    /// <param name="urls">An enumerable collection of URLs to attempt navigation to, in priority order. This collection cannot be null or empty.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task completes successfully
    /// if navigation to one of the URLs succeeds (the page redirects to the requested URL), 
    /// or throws an exception if all attempts fail.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="urls"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if navigation to all provided URLs fails, meaning all URLs in <paramref name="urls"/> could not be reached or redirected to the expected page.</exception>
    public Task GoToUrlAsync(IEnumerable<string> urls);

    /// <summary>
    /// Refreshes the current page.
    /// </summary>
    /// <returns>A task representing the asynchronous refresh operation.</returns>
    public Task RefreshAsync();

    /// <summary>
    /// Switches to the default page.
    /// </summary>
    public void SwitchToDefaultPage();

    /// <summary>
    /// Switches to the page at the specified index.
    /// </summary>
    /// <param name="index">The index of the page to switch to.</param>
    public void SwitchToPage(int index);

    /// <summary>
    /// Switches to a new page triggered by clicking the specified element.
    /// </summary>
    /// <param name="clickableElement">The clickable element that triggers the navigation.</param>
    /// <returns>A task representing the asynchronous navigation operation.</returns>
    public Task SwitchToNewPageAsync(IClickElement clickableElement);

    /// <summary>
    /// Cancels the request for the specified URL.
    /// </summary>
    /// <param name="url">The URL of the request to abort.</param>
    /// <returns>A task representing the asynchronous request cancellation operation.</returns>
    public Task CancelRequestAsync(string url);

    /// <summary>
    /// Gets the current URL of the page.
    /// </summary>
    /// <returns>The current URL of the page.</returns>
    public string CurrentUrl();

    /// <summary>
    /// Builds a URL by appending the specified route to the base URL.
    /// </summary>
    /// <param name="route">The route to be appended to the base URL.</param>
    /// <returns>The complete URL consisting of the base URL and the specified route.</returns>
    public string BuildUrlWithRoute(string route);
}