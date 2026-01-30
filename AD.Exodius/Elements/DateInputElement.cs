namespace AD.Exodius.Elements;

public class DateInputElement : BaseInputElement<DateOnly>
{
    public DateInputElement(ILocator locator) 
        : base(locator)
    {

    }

    /// <summary>
    /// <para>Performs a standard input of the element if the parameter does not contain a date of 01,01,0001, and is visible on the page.</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para>
    /// </summary>
    public override async Task VisibilityTypeInputAsync(DateOnly input)
    {
        if (!await IsVisibleAsync())
            return;

        await TypeInputAsync(input);
    }

    /// <summary>
    /// <para>Performs a standard input of the element if the parameter does not contain a date of 01,01,0001</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public override async Task TypeInputAsync(DateOnly date)
    {
        if (date == DateOnly.MinValue)
            return;

        await Locator.FillAsync(date.ToShortDateString());
    }
}
