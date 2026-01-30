namespace AD.Exodius.Elements;

public class RadioButtonElement : BaseClickElement
{
    public RadioButtonElement(ILocator locator) 
        : base(locator)
    {

    }

    public override async Task ClickAsync()
    {
        if (await Locator.IsCheckedAsync())
            return;

        await Locator.ClickAsync();
    }

    public override async Task ForceClickAsync()
    {
        if (await Locator.IsCheckedAsync())
            return;

        await FocusAsync();
        await Locator.ClickAsync(new() { Force = true });
    }
}
