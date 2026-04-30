using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public class StatusEffectValueSelector : IStatusEffectValueSelector
{
    private readonly EffectValueSelectMethod _effectValueSelectMethod;

    public StatusEffectValueSelector(EffectValueSelectMethod effectValueSelectMethod)
    {
        _effectValueSelectMethod = effectValueSelectMethod;
    }
    
    public int Select(int @base, int old, int @new)
    {
        return _effectValueSelectMethod switch
        {
            EffectValueSelectMethod.Base => @base,
            EffectValueSelectMethod.Old => old,
            EffectValueSelectMethod.New => @new,
            EffectValueSelectMethod.Max => Mathf.Max(old, @new),
            EffectValueSelectMethod.Min => Mathf.Min(old, @new),
            EffectValueSelectMethod.Sum => old + @new,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public float Select(float @base, float old, float @new)
    {
        return _effectValueSelectMethod switch
        {
            EffectValueSelectMethod.Base => @base,
            EffectValueSelectMethod.Old => old,
            EffectValueSelectMethod.New => @new,
            EffectValueSelectMethod.Max => Mathf.Max(old, @new),
            EffectValueSelectMethod.Min => Mathf.Min(old, @new),
            EffectValueSelectMethod.Sum => old + @new,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}