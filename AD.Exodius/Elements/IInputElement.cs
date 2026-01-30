namespace AD.Exodius.Elements;

public interface IInputElement<T>
{
    public Task TypeInputAsync(T input);
    public Task VisibilityTypeInputAsync(T input);
    public Task PressKeyAsync(KeyCode key);
}
