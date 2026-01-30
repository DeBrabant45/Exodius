using AD.Exodius.Drivers;
using AD.Exodius.Events;

namespace AD.Exodius.Entities.Pages.Factories;

/// <summary>
/// Represents a factory responsible for creating instances of page entities.
/// </summary>
/// <typeparam name="TPage">The type of the page entity to create, which must implement <see cref="IPageEntity"/>.</typeparam>
/// <returns>An instance of the requested page entity type <typeparamref name="TPage"/>.</returns>
/// <author>Aaron DeBrabant</author>
public interface IPageEntityFactory
{
    /// <summary>
    /// Creates an instance of a page entity.
    /// </summary>
    /// <typeparam name="TPage">The type of the page entity to create.</typeparam>
    /// <returns>An instance of the page entity <typeparamref name="TPage"/>.</returns>
    public TPage Create<TPage>(IDriver driver, IEventBus eventBus) where TPage : IPageEntity;
}
