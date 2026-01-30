using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AD.Exodius.Entities.Pages.Attributes;

namespace AD.Exodius.Entities.Pages.Extensions;

public static class PageEntityExtensions
{
    /// <summary>
    /// Retrieves the route associated with the specified page type using the <see cref="PageEntityRouteAttribute"/>.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity.</typeparam>
    /// <param name="page">The page instance for which to get the route.</param>
    /// <returns>The route associated with the page.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no route is found for the specified page type.</exception>
    public static string GetRoute<TPage>(this TPage page) where TPage : IPageEntity
    {
        var routeAttribute = typeof(TPage).GetCustomAttribute<PageEntityRouteAttribute>();

        return routeAttribute != null
            ? routeAttribute.Route
            : throw new InvalidOperationException($"No route found for the page {typeof(TPage).Name}");
    }

    /// <summary>
    /// Attempts to retrieve the route defined by the <see cref="PageEntityRouteAttribute"/> for the specified page type.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity, which must implement <see cref="IPageEntity"/>.</typeparam>
    /// <param name="page">The instance of the page entity.</param>
    /// <param name="route">
    /// When this method returns, contains the route if the attribute is found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the route is successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryGetRoute<TPage>(this TPage page, [NotNullWhen(true)] out string? route) where TPage : IPageEntity
    {
        var attribute = typeof(TPage).GetCustomAttribute<PageEntityRouteAttribute>();
        route = attribute?.Route;

        return attribute != null;
    }

    /// <summary>
    /// Retrieves the name associated with the specified page type using the <see cref="PageEntitytNameAttribute"/>.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity.</typeparam>
    /// <param name="page">The page instance for which to get the name.</param>
    /// <returns>The name associated with the page.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no name is found for the specified page type.</exception>
    public static string GetName<TPage>(this TPage page) where TPage : IPageEntity
    {
        var nameAttribute = typeof(TPage).GetCustomAttribute<PageEntitytNameAttribute>();

        return nameAttribute != null
            ? nameAttribute.Name
            : throw new InvalidOperationException($"No name found for the page {typeof(TPage).Name}");
    }

    /// <summary>
    /// Attempts to retrieve the name of the page from the <see cref="PageEntitytNameAttribute"/> applied to the page type.
    /// </summary>
    /// <typeparam name="TPage">The type of the page implementing <see cref="IPageEntity"/>.</typeparam>
    /// <param name="page">The page instance from which to retrieve the name.</param>
    /// <param name="name">
    /// When this method returns, contains the name specified in the <see cref="PageEntitytNameAttribute"/> 
    /// if the attribute is present on the page type; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the <see cref="PageEntitytNameAttribute"/> is found on the page type; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryGetName<TPage>(this TPage page, [NotNullWhen(true)] out string? name) where TPage : IPageEntity
    {
        var attribute = typeof(TPage).GetCustomAttribute<PageEntitytNameAttribute>();
        name = attribute?.Name;

        return attribute != null;
    }

    /// <summary>
    /// Retrieves the <see cref="PageEntityMetaAttribute"/> associated with the specified page type.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity.</typeparam>
    /// <param name="page">The instance of the page entity.</param>
    /// <returns>The <see cref="PageEntityMetaAttribute"/> associated with the page type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no <see cref="PageEntityMetaAttribute"/> is found for the specified page type.
    /// </exception>
    public static PageEntityMetaAttribute GetPageEntityMeta<TPage>(this TPage page) where TPage : IPageEntity
    {
        return typeof(TPage).GetCustomAttribute<PageEntityMetaAttribute>()
            ?? throw new InvalidOperationException($"No page entity Meta found for the page {typeof(TPage).Name}");
    }

    /// <summary>
    /// Attempts to retrieve the <see cref="PageEntityMetaAttribute"/> from the specified page entity type.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity that is being checked for the attribute.</typeparam>
    /// <param name="page">The instance of the page entity. This parameter is required for method syntax but not used directly.</param>
    /// <param name="meta">When this method returns, contains the <see cref="PageEntityMetaAttribute"/> if found; otherwise, <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="PageEntityMetaAttribute"/> is found on the page entity type; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryGetPageEntityMeta<TPage>(this TPage page, [NotNullWhen(true)] out PageEntityMetaAttribute? meta) where TPage : IPageEntity
    {
        meta = typeof(TPage).GetCustomAttribute<PageEntityMetaAttribute>();

        return meta != null;
    }
}
