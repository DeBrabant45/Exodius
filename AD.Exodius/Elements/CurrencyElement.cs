namespace AD.Exodius.Elements;

public class CurrencyElement : BaseInputElement<decimal>
{
    public CurrencyElement(ILocator locator) 
        : base(locator)
    {

    }

    public override async Task VisibilityTypeInputAsync(decimal input)
    {
        if (!await IsVisibleAsync())
            return;

        await TypeInputAsync(input);
    }

    public override async Task TypeInputAsync(decimal input)
    {
        await Locator.FillAsync(input.ToString("0.00").Replace(".00", string.Empty));
    }
}
