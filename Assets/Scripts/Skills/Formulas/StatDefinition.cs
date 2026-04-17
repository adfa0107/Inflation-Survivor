using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Formulas;

[Serializable]
public class StatDefinition : IFormulaDefinition<SkillContext>
{
    [SerializeField] private ValueSource source;
    [SerializeField] private StatType statType;
    [SerializeField] private float coefficient = 1f;
    
    public IFormula<SkillContext> Compile() => new Stat(source, statType, coefficient);
}