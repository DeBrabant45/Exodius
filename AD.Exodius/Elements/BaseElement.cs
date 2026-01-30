using System.Text.RegularExpressions;

namespace AD.Exodius.Elements;

public abstract class BaseElement : IElement
{
    protected readonly ILocator Locator;

    public BaseElement(ILocator locator)
    {
        Locator = locator;
    }

    public async Task<string> GetValueAsync() => await Locator.InputValueAsync();

    public async Task<string> GetTextAsync() => await Locator.InnerTextAsync();

    public async Task<string> FindTextAsync()
    {
        if (!await IsVisibleAsync())
            return string.Empty;

        return await GetTextAsync();
    }

    public async Task<string> GetNumericTextAsync() => ExtractNumericValues(await GetTextAsync());

    public async Task<string> FindNumericTextAsync() => ExtractNumericValues(await FindTextAsync());

    public async Task<bool> IsEnabledAsync() => await Locator.IsEnabledAsync();

    public async Task<bool> IsAttributePresentAsync(string attribute, string value) => await Locator.GetAttributeAsync(attribute) == value;

    public async Task<bool> IsVisibleAsync() => await Locator.IsVisibleAsync();

    public async Task<bool> IsHiddenAsync() => await Locator.IsHiddenAsync();

    public async Task FocusAsync() => await Locator.FocusAsync();

    public async Task HoverAsync() => await Locator.HoverAsync();

    public async Task WaitUntilVisibleAsync(float timeout = 30f)
    {
        await WaitUntilAsync(timeout, WaitForSelectorState.Visible);
    }

    public async Task WaitUntilHiddenAsync(float timeout = 30f)
    {
        await WaitUntilAsync(timeout, WaitForSelectorState.Hidden);
    }

    private async Task WaitUntilAsync(float timeout, WaitForSelectorState state)
    {
        await Locator.WaitForAsync(new() { State = state, Timeout = timeout * 1000 });
    }

    public async Task WaitUntilVisibleNoExceptionAsync(float timeout = 30f)
    {
        await WaitUntilNoExceptionAsync(WaitUntilVisibleAsync, timeout);
    }

    public async Task WaitUntilHiddenNoExceptionAsync(float timeout = 30f)
    {
        await WaitUntilNoExceptionAsync(WaitUntilHiddenAsync, timeout);
    }

    private async Task WaitUntilNoExceptionAsync(Func<float, Task> action, float timeout = 30f)
    {
        try
        {
            await action(timeout);
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected string ExtractNumericValues(string text) => Regex.Match(text.Replace(",", ""), @"\d+|N/A").Value;
}