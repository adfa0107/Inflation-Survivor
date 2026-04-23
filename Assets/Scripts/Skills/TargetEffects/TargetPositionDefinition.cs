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
    
    public override TargetEffect Build()
    {
        var builtEffects = new PositionEffect[effects.Length];
        
        for (int i = 0; i < effects.Length; i++)
        {
            builtEffects[i] = effects[i].Build();
        }
        
        return new TargetPosition(builtEffects);
    }
}