using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities.Pages;
using AD.Exodius.Entities.Pages.Extensions;

namespace AD.Exodius.Navigators.Strategies;

/// <summary>
/// Implements navigation by triggering a UI action.
/// </summary>
public class ByAction : INavigationStrategy
{
    public async Task Navigate<TPage>(IDriver driver, TPage page) where TPage : IPageEntity
    {
        var pageEntityLocator = page.TryGetName(out var name) 
            ? name : page.TryGetPageEntityMeta(out var meta) ? meta.DomId : null;

        if (string.IsNullOrEmpty(pageEntityLocator))
            throw new InvalidOperationException($"Page meta data is missing for {typeof(ByAction).Name} on {typeof(TPage).Name}.");

        var navigationActionComponent = page.GetComponent<INavigationActionComponent>();
        await navigationActionComponent.ClickAction(pageEntityLocator);
    }
}
