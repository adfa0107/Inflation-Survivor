using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Data.Stats;

[Serializable]
public struct StatFormulaModifierDefinition<TContext> where TContext : struct
{
    [SerializeField] private StatType statType;
    [SerializeField] private StatModifierType statModifierType;
    [SerializeField, SerializeReference, FormulaSelector] 
    private IFormulaDefinition<TContext> value;

    public StatFormulaModifier<TContext> Build()
    {
        return new StatFormulaModifier<TContext>(statType, statModifierType, value.Build());
    }
}