using InflationSurvivor.Combat;
using InflationSurvivor.EventSystem;

namespace InflationSurvivor.CombatSystem.Events;

public struct AttackEvent : IEvent
{
    public CombatModule attacker;
    public CombatModule target;
    
    public float damage;
    public void RaiseToTarget(GameEvent @event)
    {
        attacker.eventHandler.Raise(@event);
        target.eventHandler.Raise(@event);
    }
}