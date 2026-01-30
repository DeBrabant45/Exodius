using AD.Exodius.Drivers;
using AD.Exodius.Navigators.Strategies;
using NSubstitute;
using AD.Exodius.Tests.Stubs.Entities.PageObjects;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Navigators.Strategies;

public class ByRouteTests
{
    private readonly ByRoute _sut;
    private readonly IDriver _mockDriver;
    private readonly IEventBus _mockEventBus;
    private readonly StubBasicAttributePageEntity _stubAttributePage;

    public ByRouteTests()
    {
        _sut = new ByRoute();
        _mockDriver = Substitute.For<IDriver>();
        _mockEventBus = Substitute.For<IEventBus>();
        _stubAttributePage = new StubBasicAttributePageEntity(_mockDriver, _mockEventBus);
    }

    [Fact]
    public async Task Navigate_Should_GoToCorrectUrl()
    {
        var expectedRoute = "/basic";
        var expectedUrl = "http://example.com/basic";

        _mockDriver.BuildUrlWithRoute(expectedRoute).Returns(expectedUrl);

        await _sut.Navigate(_mockDriver, _stubAttributePage);

        await _mockDriver.Received(1).GoToUrlAsync(expectedUrl);
    }
}