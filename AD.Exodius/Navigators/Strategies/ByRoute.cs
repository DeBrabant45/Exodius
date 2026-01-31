using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Extensions;

namespace AD.Exodius.Navigators.Strategies;

/// <summary>
/// Implements navigation by constructing and navigating to a URL route.
/// </summary>
public class ByRoute : INavigationStrategy
{
    public async Task Navigate<TPage>(IDriver driver, TPage page) where TPage : IPageEntity
    {
        var isEntityMetaPresent = page.TryGetPageEntityMeta(out var entityMeta);
        var pageEntityMetaRoute = page.TryGetRoute(out var route) ? route : isEntityMetaPresent ? entityMeta?.Route : null;

        if (string.IsNullOrEmpty(pageEntityMetaRoute))
            throw new InvalidOperationException($"No Page Meta data as been supplied for Routing on {typeof(TPage).Name}!");

        if (isEntityMetaPresent && !string.IsNullOrEmpty(entityMeta?.QueryString))
        {
            var trimmedQueryString = entityMeta.QueryString.TrimStart('?');
            var separator = pageEntityMetaRoute.Contains('?') ? "&" : "?";
            pageEntityMetaRoute += separator + trimmedQueryString;
        }

        var newFullPath = driver.BuildUrlWithRoute(pageEntityMetaRoute);
        var currentPath = driver.CurrentUrl();

        if (currentPath == newFullPath)
            return;

        await driver.GoToUrlAsync(newFullPath);
    }
}
