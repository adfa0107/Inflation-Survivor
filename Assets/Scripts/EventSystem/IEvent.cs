namespace InflationSurvivor.EventSystem;

public interface IEvent
{
    public void RaiseToTarget(GameEvent @event);
}