namespace AD.Exodius.Drivers.Services;

/// <summary>
/// Represents a service for storing session state.
/// </summary>
/// <author>Aaron DeBrabant</author>
public interface IStorageService
{
    /// <summary>
    /// Stores the session cookie state to be used for tests to bypass authentication.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of saving the cookie session.</returns>
    public Task SaveCookieSessionAsync();

    /// <summary>
    /// Adds a token to the page context as a cookie.
    /// </summary>
    /// <param name="tokenAccess">The access token to be added as a cookie.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task AddTokenAsync(string tokenAccess, string baseUrl);
}
