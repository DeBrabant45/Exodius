using AD.Exodius.Drivers;
using AD.Exodius.Events;
using AD.Exodius.Navigators.Strategies;
using AD.Exodius.Tests.Stubs.Entities.Pages;
using NSubstitute;

namespace AD.Exodius.Tests.Navigators.Strategies;

public class ByRouteTests
{
    private readonly ByRoute _sut;
    private readonly IDriver _mockDriver;
    private readonly IEventBus _mockEventBus;

    public ByRouteTests()
    {
        _sut = new ByRoute();
        _mockDriver = Substitute.For<IDriver>();
        _mockEventBus = Substitute.For<IEventBus>();
    }

    [Fact]
    public async Task Navigate_Should_GoToCorrectUrl()
    {
        var expectedRoute = "/basic";
        var expectedUrl = "http://example.com/basic";
        var page = new StubBasicAttributePageEntity(_mockDriver, _mockEventBus);

        _mockDriver.BuildUrlWithRoute(expectedRoute).Returns(expectedUrl);

        await _sut.Navigate(_mockDriver, page);

        await _mockDriver.Received(1).GoToUrlAsync(expectedUrl);
    }

    [Fact]
    public async Task Navigate_Should_GoToCorrectUrl_WhenQueryStringIsProvided()
    {
        var expectedRoute = "/home";
        var queryString = "?param1=value1&param2=value2";
        var expectedUrl = "http://example.com/home?param1=value1&param2=value2";
        var page = new StubMetaPageEntity(_mockDriver, _mockEventBus);

        _mockDriver.BuildUrlWithRoute(expectedRoute + queryString).Returns(expectedUrl);

        await _sut.Navigate(_mockDriver, page);

        await _mockDriver.Received(1).GoToUrlAsync(expectedUrl);
    }
}