namespace AD.Exodius.Drivers.Services;

/// <summary>
/// Represents a service for browser interaction.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IBrowserService
{
    /// <summary>
    /// Closes the current page and browser session.
    /// </summary>
    /// <remarks>
    /// This method closes the current page and its associated browser session. It also closes all other browser sessions
    /// associated with the browser instance and disposes of the browser instance itself.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation of closing the page and browser.</returns>
    public Task ClosePageAsync();

    /// <summary>
    /// Opens a new browser session and creates a new page.
    /// </summary>
    /// <remarks>
    /// This method creates a new browser session using the provided browser settings and Playwright instance.
    /// It then creates a new page within the browser instance.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation of opening the browser and creating the page.</returns>
    public Task OpenPageAsync();

    /// <summary>
    /// Closes the page at the specified index without ending the browser session.
    /// </summary>
    /// <remarks>
    /// This method closes the page at the specified index within the browser context.
    /// It does not close the entire browser session or dispose of the browser instance.
    /// </remarks>
    /// <param name="index">The index of the page to close within the browser context.</param>
    /// <returns>A task representing the asynchronous operation of closing the page.</returns>
    public Task ClosePageAsync(int index);
}
