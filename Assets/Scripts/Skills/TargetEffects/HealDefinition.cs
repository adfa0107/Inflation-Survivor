using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

[Serializable]
public sealed class HealDefinition : TargetEffectDefinition
{
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<SkillContext> heal;
    
    public override TargetEffect Compile() => new Heal(heal);
}