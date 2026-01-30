namespace AD.Exodius.Events.Factories;

public class EventBusFactory : IEventBusFactory
{
    public IEventBus Create()
    {
        return new EventBus();
    }
}
