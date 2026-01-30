namespace AD.Exodius.Elements;

public class ImageElement : BaseClickElement
{
    public ImageElement(ILocator locator) 
        : base(locator)
    {

    }

    public async Task<string> FindAltTextAsync() => await Locator.GetAttributeAsync("alt") ?? "";

    public async Task<string> FindSrcAsync() => await Locator.GetAttributeAsync("src") ?? "";

    public async Task<bool> IsDataIconPresentAsync(string iconName) => await IsAttributePresentAsync("data-icon", iconName);

    public virtual async Task ClickAsyncAsync(int index) => await Locator.Nth(index).ClickAsync();
}
