using AD.Exodius.Entities.Pages.Extensions;
using AD.Exodius.Entities.Pages.Registries;

namespace AD.Exodius.Entities.Pages.Factories;

public class PageEntityRegistryFactory : IPageEntityRegistryFactory
{
    public IPageEntityRegistry? Create<TPageEntity>(TPageEntity page) where TPageEntity : IPageEntity
    {
        var hasMeta = page.TryGetPageEntityMeta(out var meta);
        var registryType = meta?.Registry;

        if (!hasMeta || registryType is null)
            return null;

        if (!typeof(IPageEntityRegistry).IsAssignableFrom(registryType))
            throw new ArgumentException($"{registryType.Name} must implement IPageEntityRegistry.");

        if (registryType.GetConstructor(Type.EmptyTypes) == null)
            throw new InvalidOperationException($"{registryType.Name} must have a parameterless constructor.");

        var instance = Activator.CreateInstance(registryType)
            ?? throw new InvalidOperationException($"Could not create instance of {registryType.Name}.");

        return (IPageEntityRegistry)instance;
    }
}
