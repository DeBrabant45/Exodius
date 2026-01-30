using NSubstitute;
using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Factories;
using AD.Exodius.Events;
using AD.Exodius.Events.Factories;
using AD.Exodius.Navigators;
using AD.Exodius.Navigators.Strategies;
using AD.Exodius.Navigators.Strategies.Factories;
using AD.Exodius.Tests.Stubs.Entities.PageObjects;
using AD.Exodius.Tests.Stubs.Navigators.Strategies;

namespace AD.Exodius.Tests.Navigators;

public class NavigatorTests
{
    private readonly Navigator _sut;
    private readonly IDriver _mockDriver;
    private readonly IEventBus _mockEventBus;
    private readonly IEventBusFactory _mockEventBusFactory;
    private readonly IPageEntityFactory _mockPageEntityFactory;
    private readonly IPageEntityRegistryFactory _mockPageEntityRegistryFactory;
    private readonly INavigationStrategyFactory _mockNavigationStrategyFactory;

    public NavigatorTests()
    {
        _mockDriver = Substitute.For<IDriver>();
        _mockEventBus = Substitute.For<IEventBus>();

        _mockEventBusFactory = Substitute.For<IEventBusFactory>();
        _mockPageEntityFactory = Substitute.For<IPageEntityFactory>();
        _mockPageEntityRegistryFactory = Substitute.For<IPageEntityRegistryFactory>();
        _mockNavigationStrategyFactory = Substitute.For<INavigationStrategyFactory>();

        _sut = new Navigator(
            _mockDriver,
            _mockEventBusFactory,
            _mockPageEntityFactory, 
            _mockPageEntityRegistryFactory, 
            _mockNavigationStrategyFactory);
    }

    [Fact]
    public async Task GoTo_Should_RunInOrder()
    {
        var stubNavigation = new StubNavigationStrategy();
        var stubPage = new StubBasicPageEntity(_mockDriver, _mockEventBus);

        _mockEventBusFactory.Create().Returns(_mockEventBus);
        _mockPageEntityFactory.Create<StubBasicPageEntity>(_mockDriver, _mockEventBus).Returns(stubPage);
        _mockNavigationStrategyFactory.Create<StubNavigationStrategy>().Returns(stubNavigation);

        await _sut.GoToAsync<StubBasicPageEntity, StubNavigationStrategy>();

        Received.InOrder(() =>
        {
            _mockEventBusFactory.Create();

            _mockPageEntityFactory.Create<StubBasicPageEntity>(_mockDriver, _mockEventBus);

            stubPage.InitializeLazyComponents();

            _mockNavigationStrategyFactory.Create<StubNavigationStrategy>();

            stubNavigation.Navigate(_mockDriver, stubPage);

            stubPage.WaitUntilReady();
        });
    }

}
