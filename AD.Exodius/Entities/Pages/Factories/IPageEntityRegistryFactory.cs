using AD.Exodius.Entities.Pages.Registries;

namespace AD.Exodius.Entities.Pages.Factories;

public interface IPageEntityRegistryFactory
{
    IPageEntityRegistry? Create<TPageEntity>(TPageEntity page) where TPageEntity : IPageEntity;
}
