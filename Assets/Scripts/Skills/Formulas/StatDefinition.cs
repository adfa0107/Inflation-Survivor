using System;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Formulas;

[Serializable]
public class StatDefinition : FormulaDefinition
{
    [SerializeField] private ValueSource source;
    [SerializeField] private StatType statType;
    [SerializeField] private float coefficient = 1f;
    
    public override Formula Compile()
    {
        return new Stat(source, statType, coefficient);
    }
}