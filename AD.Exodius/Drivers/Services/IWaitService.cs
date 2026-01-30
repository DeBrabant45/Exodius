using AD.Exodius.Elements;

namespace AD.Exodius.Drivers.Services;

/// <summary>
/// Represents a service for waiting for page events.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IWaitService
{
    /// <summary>
    /// Waits for the DOMContentLoaded event to occur.
    /// Note, this should be used sparingly as it could lead to timeouts on certain pages.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of waiting for DOMContentLoaded.</returns>
    public Task WaitForDomContentLoadedAsync();

    /// <summary>
    /// Waits for the specified timeout duration.
    /// </summary>
    /// <remarks> 
    /// Does not throw any timeout errors once timeout is met. 
    /// </remarks>
    /// <param name="timeout">The timeout duration in seconds.</param>
    /// <returns>A task representing the asynchronous operation of waiting for the specified timeout duration.</returns>
    public Task WaitForTimeoutAsync(float timeout);

    /// <summary>
    /// Waits for navigation to complete after clicking on the specified clickable element.
    /// </summary>
    /// <param name="clickableElement">The clickable element to be clicked.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method performs a click action on the specified clickable element and waits for the navigation to complete.
    /// </remarks>
    public Task WaitForNavigationAsync(IClickElement clickableElement);
}
