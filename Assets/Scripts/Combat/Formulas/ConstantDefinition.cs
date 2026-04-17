using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Formulas;

[Serializable]
public sealed class ConstantDefinition : IFormulaDefinition<SkillContext>, IFormulaDefinition<StatusEffectContext>
{
    [SerializeField] private float value;

    IFormula<SkillContext> IFormulaDefinition<SkillContext>.Compile() => new Constant(value);

    IFormula<StatusEffectContext> IFormulaDefinition<StatusEffectContext>.Compile() => new Constant(value);
}