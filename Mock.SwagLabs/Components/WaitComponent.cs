using AD.Exodius.Components;
using AD.Exodius.Entities;
using AD.Exodius.Events;

namespace Mock.SwagLabs.Components;

public class WaitComponent(IDriver driver, IEntity owner, IEventBus eventBus) 
    : EntityComponent(driver, owner, eventBus), IWaitComponent
{
    public Task WaitUntilFullyLoaded()
    {
        return Task.CompletedTask;
    }
}
