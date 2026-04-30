using InflationSurvivor.Combat.Interfaces.StatusEffect;

namespace InflationSurvivor.StatusEffects;

public class ExclusiveGroup : IExclusiveGroup
{
    public string ID { get; }
    
    public IStatusEffectSelector StatusEffectSelector { get; }
    public IStatusEffectValueSelector StackSelector { get; }
    public IStatusEffectValueSelector DurationSelector { get; }

    public ExclusiveGroup(string id, IStatusEffectSelector effectSelector, IStatusEffectValueSelector stackSelector, IStatusEffectValueSelector durationSelector)
    {
        ID = id;
        StatusEffectSelector = effectSelector;
        StackSelector = stackSelector;
        DurationSelector = durationSelector;
    }
}