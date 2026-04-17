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
public sealed class AttackDefinition : TargetEffectDefinition
{
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<SkillContext> damage;
    
    public override TargetEffect Compile() => new Attack(damage);
}