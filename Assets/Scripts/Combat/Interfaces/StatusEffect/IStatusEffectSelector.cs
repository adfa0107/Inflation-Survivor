namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IStatusEffectSelector
{
    public IStatusEffect Select(IStatusEffect old, IStatusEffect @new);
}