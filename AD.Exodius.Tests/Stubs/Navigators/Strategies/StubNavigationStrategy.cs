using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Navigators.Strategies;

namespace AD.Exodius.Tests.Stubs.Navigators.Strategies;

public class StubNavigationStrategy : INavigationStrategy
{
    public Task Navigate<TPage>(IDriver driver, TPage page) where TPage : IPageEntity
    {
        return Task.CompletedTask;
    }
}
