namespace AD.Exodius.Components.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequiresAttribute : Attribute
{
    public Type DependencyType { get; }

    public RequiresAttribute(Type dependencyType)
    {
        DependencyType = dependencyType;
    }
}
