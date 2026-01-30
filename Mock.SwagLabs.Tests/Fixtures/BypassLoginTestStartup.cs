using AD.Exodius.Helpers;
using AD.Exodius.Navigators.Strategies;
using Mock.SwagLabs.Configurations.Models;
using Mock.SwagLabs.Pages;
using Mock.SwagLabs.Pages.Mappers;

namespace Mock.SwagLabs.Tests.Fixtures;

public class BypassLoginTestStartup : BaseDashboardTestStartup
{
    protected override async Task OnSetUpAsync()
    {
        await base.OnSetUpAsync();

        var login = ApplicationSettings
            .GetFirstUser()
            .ToLogin();

        await Navigator
            .GoToAsync<LoginPage, ByRoute>()
            .Then(page => page.Login(login));
    }
}
