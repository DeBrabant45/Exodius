using AD.Exodius.Components;
using Mock.SwagLabs.Components.Enums;

namespace Mock.SwagLabs.Components;

public interface IProductSortComponent : IEntityComponent
{
    Task<IProductSortComponent> SetFilter(ProductFilter filter);
}