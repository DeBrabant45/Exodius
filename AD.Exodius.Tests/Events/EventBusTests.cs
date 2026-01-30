using NSubstitute;
using AD.Exodius.Events;

namespace AD.Exodius.Tests.Events;

public class EventBusTests
{
    private readonly EventBus _sut;

    public EventBusTests()
    {
        _sut = new EventBus();
    }

    [Fact]
    public async Task Publish_Should_InvokeSyncHandler_WhenHandlerIsSubscribed()
    {
        var handler = Substitute.For<Action<TestEvent>>();
        var testEvent = new TestEvent { Message = "Testing sync" };

        _sut.Subscribe(handler);

        await _sut.Publish(testEvent);

        handler.Received(1).Invoke(testEvent);
    }

    [Fact]
    public async Task Publish_Should_InvokeAsyncHandler_WhenHandlerIsSubscribed()
    {
        var wasCalled = false;

        _sut.SubscribeAsync<TestEvent>(async e =>
        {
            await Task.Delay(10);
            wasCalled = true;
        });

        await _sut.Publish(new TestEvent { Message = "Testing async" });

        Assert.True(wasCalled);
    }

    [Fact]
    public async Task Publish_Should_InvokeAllHandlers_WhenBothSyncAndAsyncHandlersAreSubscribed()
    {
        var syncHandler = Substitute.For<Action<TestEvent>>();
        var asyncCalled = false;

        _sut.Subscribe(syncHandler);
        _sut.SubscribeAsync<TestEvent>(async _ =>
        {
            await Task.Delay(5);
            asyncCalled = true;
        });

        var testEvent = new TestEvent { Message = "Both handlers" };
        await _sut.Publish(testEvent);

        syncHandler.Received(1).Invoke(testEvent);
        Assert.True(asyncCalled);
    }

    [Fact]
    public async Task Publish_Should_NotThrow_WhenNoHandlersAreSubscribed()
    {
        var testEvent = new TestEvent { Message = "No subscribers" };

        var ex = await Record.ExceptionAsync(() => _sut.Publish(testEvent));

        Assert.Null(ex);
    }

    [Fact]
    public async Task Publish_Should_InvokeAllSyncHandlers_WhenMultipleSyncHandlersAreSubscribed()
    {
        var handler1 = Substitute.For<Action<TestEvent>>();
        var handler2 = Substitute.For<Action<TestEvent>>();
        var testEvent = new TestEvent { Message = "Multiple sync" };

        _sut.Subscribe(handler1);
        _sut.Subscribe(handler2);

        await _sut.Publish(testEvent);

        handler1.Received(1).Invoke(testEvent);
        handler2.Received(1).Invoke(testEvent);
    }

    [Fact]
    public async Task Publish_Should_InvokeAllAsyncHandlers_WhenMultipleAsyncHandlersAreSubscribed()
    {
        var called1 = false;
        var called2 = false;

        _sut.SubscribeAsync<TestEvent>(async _ =>
        {
            await Task.Delay(1);
            called1 = true;
        });

        _sut.SubscribeAsync<TestEvent>(async _ =>
        {
            await Task.Delay(1);
            called2 = true;
        });

        await _sut.Publish(new TestEvent { Message = "Multiple async" });

        Assert.True(called1);
        Assert.True(called2);
    }

    [Fact]
    public async Task Publish_Should_NotInvokeHandler_WhenSubscribedToDifferentEventType()
    {
        var handler = Substitute.For<Action<TestEvent>>();
        _sut.Subscribe(handler);

        await _sut.Publish(new UnrelatedEvent());

        handler.DidNotReceiveWithAnyArgs().Invoke(default!);
    }

    [Fact]
    public async Task Publish_Should_InvokeAsyncHandlerWithCorrectEvent_WhenSubscribed()
    {
        TestEvent? receivedEvent = null;

        _sut.SubscribeAsync<TestEvent>(async e =>
        {
            await Task.Delay(1);
            receivedEvent = e;
        });

        var testEvent = new TestEvent { Message = "Verify payload" };

        await _sut.Publish(testEvent);

        Assert.Equal("Verify payload", receivedEvent?.Message);
    }

    [Fact]
    public async Task Publish_Should_Throw_WhenAsyncHandlerThrows()
    {
        _sut.SubscribeAsync<TestEvent>(async _ =>
        {
            await Task.Delay(1);
            throw new InvalidOperationException("boom");
        });

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.Publish(new TestEvent()));

        Assert.Equal("boom", ex.Message);
    }

    private class TestEvent : IEvent
    {
        public string Message { get; set; } = string.Empty;
    }

    private class UnrelatedEvent : IEvent { }
}
