using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Attributes;
using AD.Exodius.Entities.Pages.Registries;
using AD.Exodius.Events;
using Mock.SwagLabs.Components;

namespace Mock.SwagLabs.Pages;

[PageEntityMeta(
    Route = "/inventory.html",
    Name = "Products",
    DomId = "inventory-sidebar-link",
    Registry = typeof(ProductsPageRegistry)
)]
public class ProductsPage : PageEntity
{
    public ProductsPage(IDriver driver, IEventBus eventBus) 
        : base(driver, eventBus)
    {

    }
}

public class ProductsPageRegistry : IPageEntityRegistry
{
    public void RegisterComponents<TPage>(TPage page) where TPage : IPageEntity
    {
        page.AddComponent<WaitComponent>();
        page.AddComponent<ProductSortComponent>();
        page.AddComponent<InventoryComponent>();
        page.AddComponent<NavigationActionComponent>();
    }
}
