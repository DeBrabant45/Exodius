namespace AD.Exodius.Entities.Pages.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PageEntityRouteAttribute : Attribute
{
    public string Route { get; }

    public PageEntityRouteAttribute(string route)
    {
        Route = route;
    }
}
