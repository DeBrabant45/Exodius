using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Factories;
using AD.Exodius.Events.Factories;
using AD.Exodius.Navigators.Strategies;
using AD.Exodius.Navigators.Strategies.Factories;

namespace AD.Exodius.Navigators;

public class Navigator : INavigator
{
    protected readonly IDriver Driver;
    protected readonly IEventBusFactory EventBusFactory;
    protected readonly IPageEntityFactory PageEntityFactory;
    protected readonly IPageEntityRegistryFactory PageEntityRegistryFactory;
    protected readonly INavigationStrategyFactory NavigationStrategyFactory;

    public Navigator(
        IDriver driver,
        IEventBusFactory eventBusFactory,
        IPageEntityFactory pageEntityFactory,
        IPageEntityRegistryFactory pageEntityRegistryFactory,
        INavigationStrategyFactory navigationStrategyFactory)
    {
        Driver = driver;
        EventBusFactory = eventBusFactory;
        PageEntityFactory = pageEntityFactory;
        PageEntityRegistryFactory = pageEntityRegistryFactory;
        NavigationStrategyFactory = navigationStrategyFactory;
    }

    public virtual async Task<TPage> GoToAsync<TPage, TNavigation>()
        where TPage : IPageEntity
        where TNavigation : INavigationStrategy
    {
        var eventBus = EventBusFactory.Create();
        var pageEntity = PageEntityFactory.Create<TPage>(Driver, eventBus);

        var pageEntityRegistry = PageEntityRegistryFactory.Create(pageEntity);
        pageEntityRegistry?.RegisterComponents(pageEntity);

        pageEntity.AssembleGraph();
        pageEntity.InitializeLazyComponents();

        var navigation = NavigationStrategyFactory.Create<TNavigation>();
        await navigation.Navigate(Driver, pageEntity);

        await pageEntity.WaitUntilReady();

        return pageEntity;
    }
}