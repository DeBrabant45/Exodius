using AD.Exodius.Helpers;
using AD.Exodius.Navigators.Strategies;
using Mock.SwagLabs.Components;
using Mock.SwagLabs.Components.Enums;
using Mock.SwagLabs.Pages;
using Mock.SwagLabs.Tests.Assertions;
using Mock.SwagLabs.Tests.Fixtures;
using NUnit.Framework;

namespace Mock.SwagLabs.Tests.Products;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ProductSortFilterTests : BypassLoginTestStartup
{
    [TestCase(ProductFilter.AZ, SortOrder.Ascending)]
    [TestCase(ProductFilter.ZA, SortOrder.Descending)]
    public async Task ProductSortFilter_ShouldSortItemNames_WhenFilterIsApplied_AccordingToSortOrder(
        ProductFilter productFilter,
        SortOrder expectedSortOrder)
    {
        var productsPage = await Navigator
            .GoToAsync<ProductsPage, ByAction>();

        var productSortFilter = await productsPage
            .GetComponent<IProductSortComponent>()
            .Then(component => component.SetFilter(productFilter));

        var allItemNames = await productsPage
            .GetComponent<IInventoryComponent>()
            .Then(component => component.GetAllItemNamesInOrder());

        allItemNames
            .Should()
            .BeInOrder(expectedSortOrder);
    }

    [TestCase(ProductFilter.LoHi, SortOrder.Ascending)]
    [TestCase(ProductFilter.HiLo, SortOrder.Descending)]
    public async Task ProductSortFilter_ShouldSortItemPrices_WhenFilterIsApplied_AccordingToSortOrder(
        ProductFilter productFilter,
        SortOrder expectedSortOrder)
    {
        var productsPage = await Navigator
            .GoToAsync<ProductsPage, ByRoute>();

        var productSortFilter = await productsPage
            .GetComponent<IProductSortComponent>()
            .Then(component => component.SetFilter(productFilter));

        var allItemPrices = await productsPage
            .GetComponent<IInventoryComponent>()
            .Then(component => component.GetAllPricesInOrder<decimal>());

        allItemPrices
            .Should()
            .BeInOrder(expectedSortOrder);
    }
}
