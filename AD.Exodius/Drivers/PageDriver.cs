using AD.Exodius.Configurations;
using AD.Exodius.Drivers.Enums;
using AD.Exodius.Drivers.Factories;
using AD.Exodius.Elements;
using AD.Exodius.Helpers;
using AD.Exodius.Locators;
using AD.Exodius.Networks;
using Serilog;

namespace AD.Exodius.Drivers;

public class PageDriver : IDriver
{
    private IPage _page = null!;
    private IBrowser _browser = null!;
    private IPlaywright _playwright = null!;
    private readonly IPageFactory _pageFactory;
    private readonly ILocatorStrategyFactory _locatorStrategyFactory;
    private readonly IBrowserFactory _browserFactory;
    private readonly IElementFactory _elementFactory;
    private readonly INetworkResponseFactory _networkResponseFactory;
    private readonly DriverSettings _driverSettings;
    private readonly IPathResolver _pathResolver;

    public PageDriver(
        IPageFactory pageFactory,
        ILocatorStrategyFactory locatorStrategyFactory,
        IBrowserFactory browserFactory,
        IElementFactory elementFactory,
        INetworkResponseFactory responseFactory,
        DriverSettings settings,
        IPathResolver pathResolver)
    {
        _pageFactory = pageFactory;
        _locatorStrategyFactory = locatorStrategyFactory;
        _browserFactory = browserFactory;
        _elementFactory = elementFactory;
        _networkResponseFactory = responseFactory;
        _driverSettings = settings;
        _pathResolver = pathResolver;
    }

    public async Task OpenPageAsync()
    {
        await StartNodeJsRuntimeAsync();
        _browser = await _browserFactory.CreateBrowserAsync(_playwright, _driverSettings.BrowserSettings);
        _page = await _pageFactory.CreatePage(_browser);
    }

    private async Task StartNodeJsRuntimeAsync()
    {
        if (_playwright != null)
            return;

        _playwright = await Playwright.CreateAsync();
    }

    public async Task GoToUrlAsync(string url, ErrorBehavior errorBehavior = ErrorBehavior.ThrowException)
    {
        try
        {
            await _page.GotoAsync(url);
        }
        catch (PlaywrightException) when (errorBehavior == ErrorBehavior.ThrowException)
        {
            throw;
        }
        catch (PlaywrightException ex) when (errorBehavior == ErrorBehavior.LogException)
        {
            Console.WriteLine($"Error navigating to {url}: {ex.Message}");
        }
    }

    public async Task GoToUrlAsync(IEnumerable<string> urls)
    {
        foreach (var currentUrl in urls)
        {
            try
            {
                await _page.GotoAsync(currentUrl);
                await WaitForNetworkIdleAsync(5, ErrorBehavior.LogException);

                if (CurrentUrl().Contains(currentUrl))
                    return;
            }
            catch (PlaywrightException ex)
            {
                await WaitForNetworkIdleAsync(5, ErrorBehavior.LogException);

                Console.WriteLine($"Failed to navigate to {currentUrl}: {ex.Message}");
            }
        }

        throw new InvalidOperationException(
            $"Unable to navigate to any of the provided URLs: {string.Join(", ", urls)}.");
    }

    private async Task WaitForNetworkIdleAsync(int timeoutInSeconds, ErrorBehavior errorBehavior)
    {
        try
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = timeoutInSeconds * 1000 });
        }
        catch (TimeoutException) when (errorBehavior == ErrorBehavior.ThrowException)
        {
            throw;
        }
        catch (TimeoutException ex) when (errorBehavior == ErrorBehavior.LogException)
        {
            Console.WriteLine($"Timeout while waiting for network to become idle.: {ex.Message}");
        }
    }

    public async Task ClosePageAsync()
    {
        await _page.Context.CloseAsync();
        await _page.CloseAsync();
        foreach (var context in _browser.Contexts)
        {
            await context.CloseAsync();
        }
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
    }

    public async Task RefreshAsync()
    {
        await _page.ReloadAsync();
    }

    public string CurrentUrl()
    {
        return _page.Url;
    }

    public string BuildUrlWithRoute(string route)
    {
        var currentUrl = CurrentUrl();
        var baseUrl = UrlBuilder.GetBaseUrl(currentUrl);
        var newUrl = UrlBuilder.AppendRoute(baseUrl, route);

        return newUrl;
    }

    public async Task ClosePageAsync(int index)
    {
        await _page.Context.Pages[index].CloseAsync();
    }

    public async Task WaitForDomContentLoadedAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task WaitForNavigationAsync(IClickElement clickableElement)
    {
        await _page.RunAndWaitForNavigationAsync(clickableElement.ClickAsync);
    }

    public async Task WaitForTimeoutAsync(float timeout)
    {
        await _page.WaitForTimeoutAsync(timeout);
    }

    public async Task SaveCookieSessionAsync()
    {
        await _page.Context.StorageStateAsync(new()
        {
            Path = _driverSettings.ContextSettings.StorageStatePath
        });
    }

    public async Task SwitchToNewPageAsync(IClickElement clickableElement)
    {
        _page = await _page.Context.RunAndWaitForPageAsync(clickableElement.ClickAsync);
    }

    public void SwitchToPage(int index)
    {
        _page = _page.Context.Pages[index];
    }

    public void SwitchToDefaultPage()
    {
        _page = _page.Context.Pages.First();
    }

    public async Task StartDiagnosticsAsync(string testName)
    {
        await StartTraceAsync(testName);
    }

    private async Task StartTraceAsync(string testName)
    {
        var traceSettings = _driverSettings.TraceSettings;

        if (!traceSettings.IsTraceEnabled)
            return;

        await _page.Context.Tracing.StartAsync(new()
        {
            Title = testName,
            Screenshots = traceSettings.Screenshots,
            Snapshots = traceSettings.Snapshots,
            Sources = traceSettings.Sources,
            Name = testName,
        });
    }

    public async Task StopDiagnosticsAsync(TestResults testResults)
    {
        var captureType = _driverSettings.TraceSettings.CaptureType;

        if ((testResults.HasTestFailed && captureType == CaptureType.Failure)
            || (!testResults.HasTestFailed && captureType == CaptureType.Success)
            || captureType == CaptureType.All)
        {
            var path = _pathResolver.GeneratePath(testResults.FolderPaths);
            await StopTrace(path);
            await TakeScreenshot(path);
            AddTestLogs(testResults, path);
        }
    }

    private void AddTestLogs(TestResults testResults, string path)
    {
        var textPath = _pathResolver.Append(path, "testlogs.txt");
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(textPath, outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        testResults.TestParameters.ForEach(parameter =>
        {
            var parameterName = parameter.Item1;
            var parameterValues = parameter.Item2;
            if (parameterValues.Count <= 0)
            {
                Log.Information($"Parameter: {parameterName}");
                return;
            }

            Log.Information("Parameter: {ParameterName}: {@ParameterValues}", parameterName, parameterValues);
        });

        if (!testResults.HasTestFailed)
            return;

        Log.Error($"Expected Results: {testResults.ExpectedResults}");
        Log.Error($"Stack Trace Results: {testResults.StackTrace}");
        Log.CloseAndFlush();
    }

    private async Task StopTrace(string path)
    {
        if (!_driverSettings.TraceSettings.IsTraceEnabled)
            return;

        try
        {
            var tracePath = _pathResolver.Append(path, "trace.zip");
            await _page.Context.Tracing.StopAsync(new()
            {
                Path = tracePath,
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error stopping trace: {ex.Message}", ex);
        }
    }

    private async Task TakeScreenshot(string path)
    {
        try
        {
            var screenshotPath = _pathResolver.Append(path, "screenshot.png");
            await _page.ScreenshotAsync(new()
            {
                Path = screenshotPath,
                FullPage = true,
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error taking screenshot: {ex.Message}", ex);
        }
    }

    public async Task CancelRequestAsync(string url)
    {
        await _page.RouteAsync(url, async (route) =>
        {
            await route.AbortAsync();
        });
    }

    public async Task AddTokenAsync(string tokenAccess, string baseUrl)
    {
        await _page.Context.AddCookiesAsync(new[]
        {
            new Cookie
            {
                Name = "token",
                Value = tokenAccess,
                Domain = new Uri(baseUrl).Host,
                Path = "/",
                Secure = true,
                HttpOnly = true
            }
        });
    }

    public TElement FindElement<TLocatorStrategy, TElement>(params string[] values)
        where TLocatorStrategy : LocatorStrategy
        where TElement : IElement
    {
        var locatorStrategy = _locatorStrategyFactory.Create<TLocatorStrategy>(values);
        var nativeElement = _page.Locator(locatorStrategy.Convert());

        return _elementFactory.Create<TElement>(nativeElement);
    }

    public async Task<List<TElement>> FindAllElements<TLocatorStrategy, TElement>(params string[] values)
        where TLocatorStrategy : LocatorStrategy
        where TElement : IElement
    {
        var locatorStrategy = _locatorStrategyFactory.Create<TLocatorStrategy>(values);
        var nativeElements = await _page.Locator(locatorStrategy.Convert()).AllAsync();

        return nativeElements.Select(_elementFactory.Create<TElement>).ToList();
    }

    public TNetworkResponse FindNetworkResponse<TNetworkResponse>(string networkUrl) where TNetworkResponse : INetworkResponse
    {
        var response = _page.WaitForResponseAsync(n => n.Url.Contains(networkUrl));
        return _networkResponseFactory.Create<TNetworkResponse>(response);
    }

    public List<TNetworkResponse> FindAllNetworkResponses<TNetworkResponse>(string networkUrl) where TNetworkResponse : INetworkResponse
    {
        var responses = new List<TNetworkResponse>();
        _page.Response += (_, response) =>
        {
            if (response.Url.Contains(networkUrl))
            {
                responses.Add(_networkResponseFactory.Create<TNetworkResponse>(Task.FromResult(response)));
            }
        };
        _page.Response -= (_, response) => { };

        return responses;
    }

    public void Dispose()
    {
        _playwright.Dispose();
    }
}