using AD.Exodius.Drivers;
using NSubstitute;
using AD.Exodius.Entities.Pages.Factories;
using AD.Exodius.Tests.Stubs.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Entities.Pages.Factories;

public class PageEntityFactoryTests
{
    private PageEntityFactory _sut;
    private readonly IDriver _mockedDriver;
    private readonly IEventBus _mockedEventBus;

    public PageEntityFactoryTests()
    {
        _mockedDriver = Substitute.For<IDriver>();
        _mockedEventBus = Substitute.For<IEventBus>();
        _sut = new PageEntityFactory();
    }

    [Fact]
    public void Create_ShouldReturn_ExpectedPage()
    {
        var result = _sut.Create<StubBasicPageEntity>(_mockedDriver, _mockedEventBus);
        Assert.IsType<StubBasicPageEntity>(result);
    }
}
