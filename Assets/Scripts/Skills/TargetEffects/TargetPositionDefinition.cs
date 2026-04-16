using System;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

[Serializable]
public sealed class TargetPositionDefinition : TargetEffectDefinition
{
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionEffectDefinition[] effects;
    
    public override TargetEffect Compile()
    {
        return new TargetPosition(effects);
    }
}