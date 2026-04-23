using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Targets;

[Serializable]
public sealed class TargetActionDefinition
{
    [SerializeField, SerializeReference, FormulaSelector] 
    private IFormulaDefinition<SkillContext> delay;
    [SerializeField, SerializeReference, SubclassSelector]
    private TargetSourceDefinition targetSource;
    [SerializeField, SerializeReference, SubclassSelector]
    private DirectionSelectorDefinition directionSelector;
    [SerializeField, SerializeReference, SubclassSelector]
    private TargetEffectDefinition[] effects;
    
    public TargetAction Build()
    {
        var builtEffects = new TargetEffect[effects.Length];

        for (var i = 0; i < effects.Length; i++)
        {
            builtEffects[i] = effects[i].Build();
        }
        
        return new TargetAction(delay.Build(), targetSource.Build(), directionSelector.Build(), builtEffects);
    }
}