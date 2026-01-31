namespace AD.Exodius.Entities.Pages.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PageEntityMetaAttribute : Attribute
{
    public string Route { get; set; } = string.Empty;
    public string QueryString { get; set; } = string.Empty;
    public string DomId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Type? Registry { get; set; }

    public PageEntityMetaAttribute() { }

    public PageEntityMetaAttribute(string route)
    {
        Route = route;
    }

    public PageEntityMetaAttribute(string route, string domId)
       : this(route)
    {
        DomId = domId;
    }

    public PageEntityMetaAttribute(string route, string domId, string pageName)
        : this(route, domId)
    {
        Name = pageName;
    }

    public PageEntityMetaAttribute(string route, string domId, string pageName, string queryString)
    : this(route, domId, pageName)
    {
        QueryString = queryString;
    }

    public PageEntityMetaAttribute(string route, string domId, string pageName, string queryString, Type registry)
        : this(route, domId, pageName, queryString)
    {
        Registry = registry;
    }
}
