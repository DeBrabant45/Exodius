using Microsoft.Extensions.DependencyInjection;

namespace Mock.SwagLabs.Utilities;

public static class ServiceProviderFactory
{
    private static readonly IServiceProvider _serviceProvider;

    static ServiceProviderFactory()
    {
        var services = new ServiceCollection();

        var startupType = GetStartupType();
        var startupInstance = Activator.CreateInstance(startupType);
        var configureServicesMethod = startupType.GetMethod("ConfigureServices")
            ?? throw new InvalidOperationException("ConfigureServices method not found on Startup class.");

        configureServicesMethod.Invoke(startupInstance, [services]);

        _serviceProvider = services.BuildServiceProvider();
    }

    public static IServiceScope CreateScope()
    {
        return _serviceProvider.CreateScope();
    }

    private static Type GetStartupType()
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == "Startup" && t.IsClass)
            ?? throw new InvalidOperationException("No Startup class found in the project.");
    }
}
