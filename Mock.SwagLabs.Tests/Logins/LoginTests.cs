using AD.Exodius.Helpers;
using AD.Exodius.Navigators.Strategies;
using Mock.SwagLabs.Pages;
using Mock.SwagLabs.Pages.Models;
using Mock.SwagLabs.Tests.Fixtures;
using NUnit.Framework;

namespace Mock.SwagLabs.Tests.Logins;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class LoginTests : BaseDashboardTestStartup
{
    [TestCase("standard_user", "secret_sauce")]
    [TestCase("performance_glitch_user", "secret_sauce")]
    [TestCase("problem_user", "secret_sauce")]
    public async Task User_Should_Login_Without_Errors(string username, string password)
    {
        var login = new Login { Username = username, Password = password };

        var isLoginPageErrorMessagePresent = await Navigator
            .GoToAsync<LoginPage, ByRoute>()
            .Then(page => page.Login(login))
            .Then(page => page.IsErrorMessagePresent());

        isLoginPageErrorMessagePresent
            .Should()
            .BeFalse();
    }

    [TestCase("standard_user", "junk")]
    [TestCase("standard_user", "hello")]
    public async Task User_Should_See_Login_Error_Message(string username, string password)
    {
        var login = new Login { Username = username, Password = password };

        var loginPageErrorMessage = await Navigator
            .GoToAsync<LoginPage, ByRoute>()
            .Then(page => page.Login(login))
            .Then(page => page.GetErrorMessageText());

        loginPageErrorMessage.Should().Be("Epic sadface: Username and password do not match any user in this service");
    }

    [TestCase("locked_out_user", "secret_sauce")]
    public async Task User_Should_See_Login_Locked_Out_Error_Message(string username, string password)
    {
        var login = new Login { Username = username, Password = password };

        var loginPageErrorMessage = await Navigator
            .GoToAsync<LoginPage, ByRoute>()
            .Then(page => page.Login(login))
            .Then(page => page.GetErrorMessageText());

        loginPageErrorMessage
            .Should()
            .Be("Epic sadface: Sorry, this user has been locked out.");
    }
}
