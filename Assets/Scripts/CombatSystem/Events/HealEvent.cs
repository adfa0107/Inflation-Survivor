using InflationSurvivor.EventSystem;

namespace InflationSurvivor.CombatSystem.Events;

public struct HealEvent : IEvent
{
    public CombatModule healer;
    public CombatModule target;
    
    public float healAmount;
    public void RaiseToTarget(GameEvent @event)
    {
        healer.eventModule.Raise(@event);
        target.eventModule.Raise(@event);
    }
}