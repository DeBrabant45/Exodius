namespace AD.Exodius.Elements;

public class ButtonElement : BaseClickElement
{
    public ButtonElement(ILocator locator) 
        : base(locator)
    {

    }

    public virtual async Task ClickAsync(int index) => await Locator.Nth(index).ClickAsync();
}