namespace AD.Exodius.Events;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var type = typeof(TEvent);
        if (!_handlers.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _handlers[type] = list;
        }
        list.Add(handler);
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        var type = typeof(TEvent);
        if (!_handlers.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _handlers[type] = list;
        }
        list.Add(handler);
    }

    public async Task PublishAsync<TEvent>(TEvent publishEvent) where TEvent : IEvent
    {
        var type = typeof(TEvent);

        if (!_handlers.TryGetValue(type, out var subscribers))
            return;

        var syncHandlers = subscribers.OfType<Action<TEvent>>().ToList();
        var asyncHandlers = subscribers.OfType<Func<TEvent, Task>>().ToList();

        foreach (var syncHandler in syncHandlers)
        {
            syncHandler(publishEvent);
        }

        foreach (var asyncHandler in asyncHandlers)
        {
            await asyncHandler(publishEvent);
        }
    }
}
