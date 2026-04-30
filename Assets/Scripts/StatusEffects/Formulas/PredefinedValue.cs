using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.StatusEffects.Formulas;

public class PredefinedValue : IFormula<StatusEffectContext>
{
    private readonly PredefinedValueName _valueName;

    public PredefinedValue(PredefinedValueName valueName)
    {
        _valueName = valueName;
    }
    
    public float Evaluate(StatusEffectContext context)
    {
        return _valueName switch
        {
            PredefinedValueName.Duration => context.predefinedDuration,
            PredefinedValueName.InitStack => context.predefinedInitStack,
            PredefinedValueName.MaxStack => context.predefinedMaxStack,
            PredefinedValueName.Power => context.predefinedPower,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}