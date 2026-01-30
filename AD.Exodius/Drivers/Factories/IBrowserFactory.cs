using AD.Exodius.Configurations;

namespace AD.Exodius.Drivers.Factories;

/// <summary>
/// Represents a factory for creating browsers using Playwright.
/// </summary>
/// <remarks>
/// This interface provides a method for creating and launching browsers such as Chrome, Firefox, Edge, and Chromium,
/// with customizable settings.
/// </remarks>
/// <author>Aaron DeBrabant</author>
public interface IBrowserFactory
{
    /// <summary>
    /// Creates a browser instance based on the specified Playwright instance and browser settings.
    /// </summary>
    /// <remarks>
    /// Please note, once this is called it will create and launch the browser isntance in memory. 
    /// </remarks>
    /// <param name="playwright">The Playwright instance used for creating the browser.</param>
    /// <param name="settings">The settings for configuring the browser.</param>
    ///<returns>A task representing the asynchronous operation.The task result is the created browser instance.</returns>
    Task<IBrowser> CreateBrowserAsync(IPlaywright playwright, BrowserSettings settings);
}