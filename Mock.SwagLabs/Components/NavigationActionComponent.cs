using AD.Exodius.Components;
using AD.Exodius.Entities;
using AD.Exodius.Events;
using AD.Exodius.Locators;

namespace Mock.SwagLabs.Components;

public class NavigationActionComponent(IDriver driver, IEntity owner, IEventBus eventBus) 
    : EntityComponent(driver, owner, eventBus), INavigationActionComponent
{
    private ButtonElement BurgerMenuButton => Driver.FindElement<ById, ButtonElement>("react-burger-menu-btn");
    private LabelElement MenuWrapLabel => Driver.FindElement<ByXPath, LabelElement>("//div[@class='bm-menu-wrap']");

    public async Task ClickAction(string item)
    {
        if (await MenuWrapLabel.IsAttributePresentAsync("aria-hidden", "false"))
            return;

        await BurgerMenuButton.ClickAsync();
    }
}
