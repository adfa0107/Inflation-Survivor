namespace InflationSurvivor.CombatSystem.Events;

public struct DamageEvent
{
    public CombatModule attacker;
    public CombatModule target;
    
    public float damage;
}