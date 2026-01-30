namespace Mock.SwagLabs.Utilities;

public static class EnumExtension
{
    public static string GetHtmlValue(this Enum value) => value.GetAttributeStringValue<HtmlElementValueAttribute>();

    private static string GetAttributeStringValue<TAttribute>(this Enum value) where TAttribute : Attribute, IAttributeValue
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo?.GetCustomAttributes(typeof(TAttribute), false);

        return attributes != null && attributes.Length > 0
            ? ((IAttributeValue)attributes[0]).AttributeValue
            : throw new InvalidOperationException($"{fieldInfo} does not have a custom attribute value!");
    }

    public static IEnumerable<T> GetAllValues<T>() where T : Enum
    {
        return Enum
            .GetValues(typeof(T))
            .Cast<T>();
    }
}
