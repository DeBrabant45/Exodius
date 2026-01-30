namespace AD.Exodius.Elements;

public abstract class BaseInputElement<T> : BaseElement, IInputElement<T>
{
    protected BaseInputElement(ILocator locator) 
        : base(locator)
    {

    }

    /// <summary>
    /// <para>Performs a standard input of the element if the parameter is not null or empty.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    public abstract Task TypeInputAsync(T input);

    /// <summary>
    /// <para>Performs a standard input of the element if the parameter is not null, empty and is visible on the page.</para>
    /// <para>Action will be returned and no error will be thrown if element is not present.</para>
    /// </summary>
    public abstract Task VisibilityTypeInputAsync(T input);

    /// <summary>
    /// <para>Perfroms a key press operation based on the key code supplied.</para>
    /// <para>Action will throw an error if element is not present.</para>
    /// </summary>
    /// <param name="key"></param>
    public async Task PressKeyAsync(KeyCode key) => await Locator.PressAsync(key.ToString());
}
