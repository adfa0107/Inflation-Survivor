using InflationSurvivor.EventSystem;

namespace InflationSurvivor.CombatSystem.Events;

public struct AttackEvent : IEvent
{
    public CombatModule attacker;
    public CombatModule target;
    
    public float damage;
    public void RaiseToTarget(GameEvent @event)
    {
        attacker.eventModule.Raise(@event);
        target.eventModule.Raise(@event);
    }
}