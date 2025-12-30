namespace InflationSurvivor.CombatData;

public interface ICombatModule
{
    public Stat Stat { get; }
    
    public void Damage(ICombatModule attacker, float amount);
    public void Heal(ICombatModule healer, float amount);
}