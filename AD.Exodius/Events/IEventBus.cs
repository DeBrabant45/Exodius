
namespace AD.Exodius.Events;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent publishEvent) where TEvent : IEvent;
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;
}