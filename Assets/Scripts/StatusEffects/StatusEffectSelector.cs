using System;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;

namespace InflationSurvivor.StatusEffects;

public class StatusEffectSelector : IStatusEffectSelector
{
    private readonly EffectSelectMethod _effectSelectMethod;

    public StatusEffectSelector(EffectSelectMethod effectSelectMethod)
    {
        _effectSelectMethod = effectSelectMethod;
    }
    
    public IStatusEffect Select(IStatusEffect old, IStatusEffect @new)
    {
        return _effectSelectMethod switch
        {
            EffectSelectMethod.Old => old,
            EffectSelectMethod.New => @new,
            EffectSelectMethod.HigherPriority =>
                old.Data.Priority > @new.Data.Priority ? old : @new,
            EffectSelectMethod.Longer => old.RemainingTime > @new.RemainingTime ? old : @new,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}