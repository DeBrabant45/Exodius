using AD.Exodius.Drivers;
using AD.Exodius.Events;

namespace AD.Exodius.Entities.Pages.Factories;

public class PageEntityFactory : IPageEntityFactory
{
    public TPage Create<TPage>(IDriver driver, IEventBus eventBus) where TPage : IPageEntity
    {
        ArgumentNullException.ThrowIfNull(driver);

        var instance = Activator.CreateInstance(typeof(TPage), driver, eventBus)
            ?? throw new InvalidOperationException($"No page of type {typeof(TPage).Name} found.");

        return (TPage)instance;
    }
}
