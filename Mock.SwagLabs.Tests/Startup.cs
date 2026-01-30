using Microsoft.Extensions.DependencyInjection;
using Mock.SwagLabs.StartupServices;

namespace Mock.SwagLabs.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddApplicationSettingServices();
    }
}
