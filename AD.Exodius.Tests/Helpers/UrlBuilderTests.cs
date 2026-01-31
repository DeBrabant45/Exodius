using AD.Exodius.Helpers;

namespace AD.Exodius.Tests.Helpers;

public class UrlBuilderTests
{
    [Theory]
    [InlineData("http://example.com/path/to/resource", "http://example.com")]
    [InlineData("https://example.com:8080/another/path", "https://example.com")]
    [InlineData("http://example.com/", "http://example.com")]
    [InlineData("https://example.com/some/path/", "https://example.com")]
    [InlineData("http://example.com", "http://example.com")]
    [InlineData("https://example.com", "https://example.com")]
    [InlineData("https://testing.com/secure/module/test", "https://testing.com/secure")]
    [InlineData("https://testing.com/login", "https://testing.com")]
    public void GetBaseUrl_Should_ReturnCorrectBaseUrl(string currentUrl, string expectedBaseUrl)
    {
        var result = UrlBuilder.GetBaseUrl(currentUrl);

        Assert.Equal(expectedBaseUrl, result);
    }

    [Theory]
    [InlineData("http://example1.com", "path", "http://example1.com/path/")]
    [InlineData("http://example2.com", "/path", "http://example2.com/path/")]
    [InlineData("http://example3.com", "path/", "http://example3.com/path/")]
    [InlineData("http://example4.com", "/path/", "http://example4.com/path/")]
    [InlineData("http://example5.com", "path/subpath", "http://example5.com/path/subpath/")]
    [InlineData("http://example6.com", "", "http://example6.com")]
    [InlineData("http://example7.com", null, "http://example7.com")]
    [InlineData("https://testing1.com/secure", "module/test", "https://testing1.com/secure/module/test/")]
    [InlineData("https://testing2.com/secure/", "/module/test", "https://testing2.com/secure/module/test/")]
    [InlineData("https://testing2.com", "/login", "https://testing2.com/login/")]
    [InlineData("https://testing2.com/secure", "/login", "https://testing2.com/login/")]
    [InlineData("https://testing5.com/secure", "/login?param=1", "https://testing5.com/login?param=1")]
    public void AppendRoute_Should_AppendRouteCorrectly(string baseUrl, string route, string expectedUrl)
    {
        var result = UrlBuilder.AppendRoute(baseUrl, route);

        Assert.Equal(expectedUrl, result);
    }
}
