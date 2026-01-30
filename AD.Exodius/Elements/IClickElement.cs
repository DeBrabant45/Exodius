namespace AD.Exodius.Elements;

public interface IClickElement
{
    public Task ClickAsync();
    public Task ClickAsync(bool shouldClick);
    public Task ForceClickAsync();
    public Task ForceClickAsync(bool shouldForceClick);
    public Task NoHoverClickAsync();
}
