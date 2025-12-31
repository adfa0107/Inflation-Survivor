namespace InflationSurvivor.EventSystem;

public abstract class GameEvent
{
    public static (bool isCancelled, TEvent result) RaisePrev<TEvent>(TEvent @event) where TEvent : struct, IEvent
    {
        Prev<TEvent> prevEvent = Prev<TEvent>.Get(@event);
        @event.RaiseToTarget(prevEvent);
        (bool, TEvent) result = (prevEvent.isCancelled, prevEvent.data);
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