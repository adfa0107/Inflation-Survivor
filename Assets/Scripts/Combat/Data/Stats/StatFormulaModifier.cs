using System;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Data.Stats;

public readonly struct StatFormulaModifier<TContext> where TContext : struct
{
    private readonly StatType _statType;
    private readonly StatModifierType _statModifierType;
    private readonly IFormula<TContext> _value;

    public StatFormulaModifier(StatType statType, StatModifierType statModifierType, IFormula<TContext> value)
    {
        _statType = statType;
        _statModifierType = statModifierType;
        _value = value;
    }

    public StatModifier MakeModifier(TContext context)
    {
        return new StatModifier{statType = _statType, statModifierType = _statModifierType, value = _value.Evaluate(context)};
    }
}