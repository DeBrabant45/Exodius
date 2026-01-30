using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages.Factories;
using AD.Exodius.Events.Factories;
using AD.Exodius.Navigators.Strategies.Factories;

namespace AD.Exodius.Navigators.Factories;

public class NavigatorFactory : INavigatorFactory
{
    private readonly IEventBusFactory _eventBusFactory;
    private readonly IPageEntityFactory _pageEntityFactory;
    private readonly IPageEntityRegistryFactory _pageEntityRegistryFactory;
    private readonly INavigationStrategyFactory _navigationStrategyFactory;

    public NavigatorFactory(
        IEventBusFactory? eventBusFactory = null,
        IPageEntityFactory? pageEntityFactory = null,
        IPageEntityRegistryFactory? pageEntityRegistryFactory = null,
        INavigationStrategyFactory? navigationStrategyFactory = null)
    {
        _eventBusFactory = eventBusFactory ?? new EventBusFactory();
        _pageEntityFactory = pageEntityFactory ?? new PageEntityFactory();
        _pageEntityRegistryFactory = pageEntityRegistryFactory ?? new PageEntityRegistryFactory();
        _navigationStrategyFactory = navigationStrategyFactory ?? new NavigationStrategyFactory();
    }

    public TNavigator Create<TNavigator>(IDriver driver) where TNavigator : INavigator
    {
        ArgumentNullException.ThrowIfNull(driver);

        var instance = Activator.CreateInstance(typeof(TNavigator), 
            driver,
            _eventBusFactory,
            _pageEntityFactory,
            _pageEntityRegistryFactory,
            _navigationStrategyFactory)
            ?? throw new InvalidOperationException($"Failed to create an instance of {typeof(TNavigator).Name}.");

        return (TNavigator)instance;
    }
}
