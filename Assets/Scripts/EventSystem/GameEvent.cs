namespace InflationSurvivor.EventSystem;

public abstract class GameEvent
{
    public static (bool, TEvent) RaisePrev<TEvent>(TEvent @event, bool cancelable = true) where TEvent : struct, IEvent
    {
        Prev<TEvent> prevEvent = Prev<TEvent>.Get(@event, cancelable);
        @event.RaiseToTarget(prevEvent);
        (bool, TEvent) result = (prevEvent.IsCancelled, prevEvent.data);
        prevEvent.Release();
        return result;
    }

    public static void RaisePost<TEvent>(TEvent @event) where TEvent : struct, IEvent
    {
        Post<TEvent> postEvent = Post<TEvent>.Get(@event);
        @event.RaiseToTarget(postEvent);
        postEvent.Release();
    }
}