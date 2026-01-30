using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Entities.Pages.Registries;
using AD.Exodius.Events;
using AD.Exodius.Locators;
using Mock.SwagLabs.Components;
using Mock.SwagLabs.Pages.Models;

namespace Mock.SwagLabs.Pages;

[PageEntityMeta(
    Route = "/",
    Registry = typeof(LoginPageRegistry)
)]
public class LoginPage : PageEntity
{
    public LoginPage(IDriver driver, IEventBus eventBus)
        : base(driver, eventBus)
    {

    }

    private TextInputElement UserNameTextbox => Driver.FindElement<ById,TextInputElement>("user-name");
    private TextInputElement PasswordTextbox => Driver.FindElement<ById, TextInputElement>("password");
    private ButtonElement LoginButton => Driver.FindElement<ById, ButtonElement>("login-button");
    private LabelElement ErrorMessage => Driver.FindElement<ByTestData, LabelElement>("error");

    public async Task<LoginPage> Login(Login login)
    {
        await UserNameTextbox.TypeInputAsync(login.Username);
        await PasswordTextbox.TypeInputAsync(login.Password);
        await LoginButton.ClickAsync();

        return this;
    }

    public async Task<bool> IsErrorMessagePresent() => await ErrorMessage.IsVisibleAsync();

    public async Task<string> GetErrorMessageText() => await ErrorMessage.GetTextAsync();
}

public class LoginPageRegistry : IPageEntityRegistry
{
    public void RegisterComponents<TPage>(TPage page) where TPage : IPageEntity
    {
        page.AddComponent<WaitComponent>();
    }
}