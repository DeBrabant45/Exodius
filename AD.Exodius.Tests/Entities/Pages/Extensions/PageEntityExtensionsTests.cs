using AD.Exodius.Drivers;
using NSubstitute;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Extensions;
using AD.Exodius.Tests.Stubs.Entities.Pages;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Entities.Pages.Extensions;

public class PageEntityExtensionsTests
{
    private readonly IDriver _mockedDriver;
    private readonly IEventBus _mockedEventBus;
    private readonly PageEntity _pageEntity;
    private readonly StubMetaPageEntity _metaPageEntity;
    private readonly StubBasicAttributePageEntity _attributePageObject;

    public PageEntityExtensionsTests()
    {
        _mockedDriver = Substitute.For<IDriver>();
        _mockedEventBus = Substitute.For<IEventBus>();
        _pageEntity = new PageEntity(_mockedDriver, _mockedEventBus);
        _metaPageEntity = new StubMetaPageEntity(_mockedDriver, _mockedEventBus);
        _attributePageObject = new StubBasicAttributePageEntity(_mockedDriver, _mockedEventBus);
    }

    [Fact]
    public void GetRoute_ShouldReturnValidRoute_WhenRouteIsFound()
    {
        var results = _attributePageObject.GetRoute();

        Assert.Equal("/basic", results);
    }

    [Fact]
    public void TryGetRoute_ShouldReturnTrueAndValidRoute_WhenRouteIsFound()
    {
        var isRoutePresent = _attributePageObject.TryGetRoute(out var route);

        Assert.True(isRoutePresent);
        Assert.Equal("/basic", route);
    }

    [Fact]
    public void TryGetRoute_ShouldReturnFalseAndNull_WhenRouteIsNotFound()
    {
        var isRoutePresent = _pageEntity.TryGetRoute(out var route);

        Assert.False(isRoutePresent);
        Assert.Null(route);
    }

    [Fact]
    public void GetRoute_ShouldThrownException_WhenRouteIsNotFound()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => _pageEntity.GetRoute());

        Assert.Contains($"No route found for the page {typeof(PageEntity).Name}", exception.Message);
    }

    [Fact]
    public void GetName_ShouldReturnValidName_WhenNameIsFound()
    {
        var results = _attributePageObject.GetName();

        Assert.Equal("Basic Attribute", results);
    }

    [Fact]
    public void TryGetName_ShouldReturnTrueAndValidName_WhenNameIsFound()
    {
        var isNamePresent = _attributePageObject.TryGetName(out var name);

        Assert.True(isNamePresent);
        Assert.Equal("Basic Attribute", name);
    }

    [Fact]
    public void TryGetName_ShouldReturnFalseAndNull_WhenRouteIsNotFound()
    {
        var isNamePresent = _pageEntity.TryGetName(out var name);

        Assert.False(isNamePresent);
        Assert.Null(name);
    }

    [Fact]
    public void GetName_ShouldThrownException_WhenRouteIsNotFound()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => _pageEntity.GetName());

        Assert.Contains($"No name found for the page {typeof(PageEntity).Name}", exception.Message);
    }

    [Fact]
    public void GetPageEntityMeta_ShouldReturnValidMeta_WhenMetaIsFound()
    {
        var meta = _metaPageEntity.GetPageEntityMeta();

        Assert.NotNull(meta);
        Assert.Equal("/home", meta.Route);
        Assert.Equal("Example", meta.Name);
        Assert.Equal("test", meta.DomId);
        Assert.Equal("?param1=value1&param2=value2", meta.QueryString);
    }

    [Fact]
    public void TryGetPageEntityMeta_ShouldReturnTrueAndValidMeta_WhenMetaIsFound()
    {
        var isMetaPresent = _metaPageEntity.TryGetPageEntityMeta(out var meta);

        Assert.True(isMetaPresent);
        Assert.NotNull(meta);
        Assert.Equal("/home", meta.Route);
        Assert.Equal("Example", meta.Name);
        Assert.Equal("test", meta.DomId);
        Assert.Equal("?param1=value1&param2=value2", meta.QueryString);
    }

    [Fact]
    public void TryGetPageObjectMeta_ShouldReturnFalseAndNullMeta_WhenMetaIsNotFound()
    {
        var isMetaPresent = _pageEntity.TryGetPageEntityMeta(out var meta);

        Assert.False(isMetaPresent);
        Assert.Null(meta);
    }
}
