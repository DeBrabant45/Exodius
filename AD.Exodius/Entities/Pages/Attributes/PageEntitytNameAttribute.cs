namespace AD.Exodius.Entities.Pages.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PageEntitytNameAttribute : Attribute
{
    public string Name { get; }

    public PageEntitytNameAttribute(string name)
    {
        Name = name;
    }
}
