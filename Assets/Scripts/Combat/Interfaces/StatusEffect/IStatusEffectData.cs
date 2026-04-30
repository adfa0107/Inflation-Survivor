using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IStatusEffectData : IHasID
{
    public int Priority { get; }
    public IExclusiveGroup ExclusiveGroup { get; }
    public IFormula<StatusEffectContext> InitStack { get; }
    public IFormula<StatusEffectContext> Duration { get; }
    
    public IStatusEffect Create();
}