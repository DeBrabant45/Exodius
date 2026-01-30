namespace AD.Exodius.Elements;

public class TextInputElement : BaseInputElement<string>
{
    public TextInputElement(ILocator webElement) : base(webElement)
    {

    }

    public override async Task TypeInputAsync(string input)
    {
        if (string.IsNullOrEmpty(input))
            return;

        await Locator.FillAsync(input);
    }

    public override async Task VisibilityTypeInputAsync(string input)
    {
        if (!await IsVisibleAsync())
            return;

        await TypeInputAsync(input);
    }
}
