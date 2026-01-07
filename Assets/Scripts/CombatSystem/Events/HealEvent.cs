using InflationSurvivor.EventSystem;

namespace InflationSurvivor.CombatSystem.Events;

public struct HealEvent : IEvent
{
    public CombatModule healer;
    public CombatModule target;
    
    public float healAmount;
    public void RaiseToTarget(GameEvent @event)
    {
        healer.eventHandler.Raise(@event);
        target.eventHandler.Raise(@event);
    }
}