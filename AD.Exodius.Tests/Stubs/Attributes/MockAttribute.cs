namespace AD.Exodius.Tests.Stubs.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class MockAttribute : Attribute
{
    public string Label { get; }

    public MockAttribute(string label)
    {
        Label = label;
    }
}

