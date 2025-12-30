namespace InflationSurvivor.CombatSystem.Events;

public struct HealEvent
{
    public CombatModule healer;
    public CombatModule target;
    
    public float healAmount;
}