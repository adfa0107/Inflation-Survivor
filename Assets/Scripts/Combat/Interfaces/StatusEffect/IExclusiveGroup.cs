namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IExclusiveGroup : IHasID
{
    public IStatusEffectSelector StatusEffectSelector { get; }
    public IStatusEffectValueSelector StackSelector { get; }
    public IStatusEffectValueSelector DurationSelector { get; }
}