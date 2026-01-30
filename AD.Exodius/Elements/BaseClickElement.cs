namespace AD.Exodius.Elements;

public abstract class BaseClickElement : BaseElement, IClickElement
{
    protected BaseClickElement(ILocator locator) 
        : base(locator)
    {

    }

    /// <summary>
    /// <para>Performs a double click operation on the given element.</para> 
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public virtual async Task DoubleClickAsync() => await Locator.DblClickAsync();

    /// <summary>
    /// <para>Performs a standard click operation on the given element.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public virtual async Task ClickAsync() => await Locator.ClickAsync();

    /// <summary>
    /// <para>Performs a standard click operation on the given element when parameter is true.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public virtual async Task ClickAsync(bool shouldClick)
    {
        if (!shouldClick)
            return;

        await ClickAsync();
    }

    /// <summary>
    /// <para>Bypasses built in checks to force the click operation.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public virtual async Task ForceClickAsync()
    {
        await FocusAsync();
        await Locator.ClickAsync(new() { Force = true });
    }

    /// <summary>
    /// <para>Bypasses built in checks to force the click operation when parameter is true.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public virtual async Task ForceClickAsync(bool shouldClick)
    {
        if (!shouldClick)
            return;

        await ForceClickAsync();
    }

    /// <summary>
    /// Performs a click operation on the current locator without causing a hover event.
    /// </summary>
    public virtual async Task NoHoverClickAsync()
    {
        await Locator.EvaluateAsync("element => element.click()");
    }

    /// <summary>
    /// <para>Performs a standard click if the element is visible on the page.</para> 
    /// <para>Action will be returned and no error will be thrown if element is not present.</para>
    /// </summary>
    public virtual async Task VisibilityClickAsync()
    {
        if (!await IsVisibleAsync())
            return;

        await ClickAsync();
    }

    /// <summary>
    /// <para>Performs a standard click if the parameter is true and element is visible on the page</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para> 
    /// </summary>
    /// <param name="shouldClick"></param>
    public virtual async Task VisibilityClickAsync(bool shouldClick)
    {
        if (!shouldClick)
            return;

        await VisibilityClickAsync();
    }

    /// <summary>
    /// Clicks the button if it is both visible and enabled.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task ReadyClickAsync()
    {
        if (!await IsVisibleAsync() || !await IsEnabledAsync())
            return;

        await ClickAsync();
    }

    /// <summary>
    /// Clicks the button if it is both visible and enabled, and based on the specified condition.
    /// </summary>
    /// <param name="shouldClick">Indicates whether the button should be clicked.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task ReadyClickAsync(bool shouldClick)
    {
        if (!shouldClick)
            return;

        await ReadyClickAsync();
    }
}
