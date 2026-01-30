namespace AD.Exodius.Navigators.Strategies.Factories;

public class NavigationStrategyFactory : INavigationStrategyFactory
{
    public INavigationStrategy Create<TNavigation>() where TNavigation : INavigationStrategy
    {
        var instance = Activator.CreateInstance(typeof(TNavigation))
            ?? throw new InvalidOperationException($"Failed to create an instance of {typeof(TNavigation).Name}.");

        return (TNavigation)instance;
    }
}
