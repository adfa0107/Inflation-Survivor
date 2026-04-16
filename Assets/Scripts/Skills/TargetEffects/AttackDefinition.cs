using System;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

[Serializable]
public sealed class AttackDefinition : TargetEffectDefinition
{
    [SerializeField, SerializeReference, SubclassSelector]
    private FormulaDefinition damage;
    
    public override TargetEffect Compile()
    {
        return new Attack(damage);
    }
}