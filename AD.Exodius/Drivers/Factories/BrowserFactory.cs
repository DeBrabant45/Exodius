using AD.Exodius.Configurations;

namespace AD.Exodius.Drivers.Factories;

public class BrowserFactory : IBrowserFactory
{
    public async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright, BrowserSettings settings)
    {
        return settings.Browser switch
        {
            Browser.Chrome => await CreateChromeDriverAsync(playwright, settings),
            Browser.Firefox => await CreateFirefoxDriverAsync(playwright, settings),
            Browser.Edge => await CreateEdgeDriverAsync(playwright, settings),
            Browser.Chromium => await CreateChromiumDriverAsync(playwright, settings),
            _ => await CreateChromiumDriverAsync(playwright, settings)
        };
    }

    private async Task<IBrowser> CreateChromeDriverAsync(IPlaywright playwright, BrowserSettings settings)
    {
        var options = CreateBrowserTypeOtions(settings);
        options.Channel = "chrome";

        return await playwright.Chromium.LaunchAsync(options);
    }

    private async Task<IBrowser> CreateFirefoxDriverAsync(IPlaywright playwright, BrowserSettings settings)
    {
        var options = CreateBrowserTypeOtions(settings);
        options.Channel = "firefox";

        return await playwright.Firefox.LaunchAsync(options);
    }

    private async Task<IBrowser> CreateChromiumDriverAsync(IPlaywright playwright, BrowserSettings settings)
    {
        var options = CreateBrowserTypeOtions(settings);
        options.Channel = "chromium";

        return await playwright.Chromium.LaunchAsync(options);
    }

    private async Task<IBrowser> CreateEdgeDriverAsync(IPlaywright playwright, BrowserSettings settings)
    {
        var options = CreateBrowserTypeOtions(settings);
        options.Channel = "msedge";

        return await playwright.Chromium.LaunchAsync(options);
    }

    private BrowserTypeLaunchOptions CreateBrowserTypeOtions(BrowserSettings settings)
    {
        return new BrowserTypeLaunchOptions
        {
            Args = settings.Args,
            Timeout = ToMilliseconds(settings.Timeout),
            Headless = settings.Headless,
            SlowMo = settings.SlowMotion,
        };
    }

    private float? ToMilliseconds(float? seconds) => seconds * 1000;
}