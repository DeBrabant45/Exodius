namespace AD.Exodius.Elements;

public class CheckboxElement : BaseElement
{
    public CheckboxElement(ILocator locator) : base(locator)
    {

    }

    /// <summary>
    /// <para>Performs a standard check.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public async Task CheckAsync() => await Locator.CheckAsync();

    /// <summary>
    /// <para>Performs a standard uncheck.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public async Task UncheckAsync() => await Locator.UncheckAsync();

    /// <summary>
    /// <para>Performs a standard uncheck if element is visible on the page.</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para> 
    /// </summary>
    public async Task VisibilityUncheckAsync()
    {
        if (!await IsVisibleAsync())
            return;

        await UncheckAsync();
    }

    /// <summary>
    /// <para>Performs a standard check if element is visible on the page.</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para> 
    /// </summary>
    public async Task VisibilityCheckAsync()
    {
        if (!await IsVisibleAsync())
            return;

        await CheckAsync();
    }

    /// <summary>
    /// <para>Performs a standard check/uncheck.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public async Task SetCheckedAsync(bool enable)
    {
        if (enable)
        {
            await CheckAsync();
            return;
        }
        await UncheckAsync();
    }

    /// <summary>
    /// <para>Performs a standard check/uncheck if element is visible on the page.</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para> 
    /// </summary>
    public async Task VisibilitySetCheckedAsync(bool enable)
    {
        if (!await IsVisibleAsync())
            return;

        await SetCheckedAsync(enable);
    }
}