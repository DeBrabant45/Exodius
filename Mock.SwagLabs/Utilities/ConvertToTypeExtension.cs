using System.Globalization;

namespace Mock.SwagLabs.Utilities;

public static class ConvertToTypeExtension
{
    private static readonly int _decimalPlaces = 1;

    public static TPrimitive ConvertToType<TPrimitive, TInput>(this TInput? amount)
        where TInput : struct
        where TPrimitive : struct
    {
        return amount.HasValue
            ? amount.Value.ConvertToType<TPrimitive, TInput>()
            : default;
    }

    public static TPrimitive ConvertToType<TPrimitive, TInput>(this TInput amount) where TInput : struct
    {
        return ConvertValue<TPrimitive>(amount);
    }

    public static TPrimitive ConvertToType<TPrimitive>(this string amount)
    {
        if (string.IsNullOrWhiteSpace(amount))
            return default!;

        if (Type.GetTypeCode(typeof(TPrimitive)) == TypeCode.String)
            return (TPrimitive)(object)amount;

        var sanitizedAmount = NumericSanitizer.Sanitize(amount);

        if (string.IsNullOrWhiteSpace(sanitizedAmount))
            throw new ArgumentException($"The sanitized amount resulted in an empty string. Input value: '{amount}'.");

        return ConvertValue<TPrimitive>(sanitizedAmount);
    }

    private static TPrimitive ConvertValue<TPrimitive>(object value)
    {
        return Type.GetTypeCode(typeof(TPrimitive)) switch
        {
            TypeCode.Int32 => (TPrimitive)(object)TruncateToInt(value),
            TypeCode.Int64 => (TPrimitive)(object)TruncateToLong(value),
            TypeCode.Double => (TPrimitive)(object)TruncateToDecimals(Convert.ToDouble(value), _decimalPlaces),
            TypeCode.Single => (TPrimitive)(object)TruncateToDecimals(Convert.ToSingle(value), _decimalPlaces),
            TypeCode.Decimal => (TPrimitive)(object)TruncateToDecimals(Convert.ToDecimal(value), _decimalPlaces),
            TypeCode.String => (TPrimitive)(object)FormatStringValue(value),
            _ => throw new ArgumentException($"Type {typeof(TPrimitive)} is not supported.")
        };
    }

    private static int TruncateToInt(object value)
    {
        var converted = Convert.ToDouble(value, CultureInfo.InvariantCulture);
        return (int)Math.Truncate(converted);
    }

    private static long TruncateToLong(object value)
    {
        if (value is string stringValue && decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
            return (long)Math.Truncate(decimalValue);

        var converted = Convert.ToDouble(value, CultureInfo.InvariantCulture);
        return (long)Math.Truncate(converted);
    }

    private static float TruncateToDecimals(float value, int decimalPlaces)
    {
        float multiplier = (float)Math.Pow(10, decimalPlaces);
        return (float)Math.Truncate(value * multiplier) / multiplier;
    }

    private static double TruncateToDecimals(double value, int decimalPlaces)
    {
        double multiplier = Math.Pow(10, decimalPlaces);
        return Math.Truncate(value * multiplier) / multiplier;
    }

    private static decimal TruncateToDecimals(decimal value, int decimalPlaces)
    {
        decimal multiplier = (decimal)Math.Pow(10, decimalPlaces);
        return Math.Truncate(value * multiplier) / multiplier;
    }

    private static string FormatStringValue(object value)
    {
        return value switch
        {
            float f => string.Format("{0:#,0.0}", f),
            double d => string.Format("{0:#,0.0}", d),
            _ => string.Format("{0:#,0}", value)
        };
    }
}
