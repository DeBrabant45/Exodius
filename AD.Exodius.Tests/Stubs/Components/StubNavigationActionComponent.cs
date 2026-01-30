using AD.Exodius.Components;
using AD.Exodius.Drivers;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Stubs.Components;

public class StubNavigationActionComponent(IDriver driver, IEntity owner, IEventBus eventBus) 
    : EntityComponent(driver, owner, eventBus), INavigationActionComponent
{
    public Task ClickAction(string item)
    {
        return Task.CompletedTask;
    }
}
