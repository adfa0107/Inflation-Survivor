namespace InflationSurvivor.Combat.Interfaces;

public interface IStatusEffectData : IHasID
{
    public IStatusEffect Create(CombatModule owner);
}