using System;
using InflationSurvivor.Core.Attributes;
using SerializeReferenceEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace InflationSurvivor.Skills.Primitives.Targets;

[Serializable]
public sealed class TargetActionDefinition
{
    [SerializeField, SerializeReference, SubclassSelector] 
    private FormulaDefinition delay;
    [FormerlySerializedAs("targetSelector")] [SerializeField, SerializeReference, SubclassSelector]
    private TargetSourceDefinition targetSource;
    [SerializeField, SerializeReference, SubclassSelector]
    private DirectionSelectorDefinition directionSelector;
    [SerializeField, SerializeReference, SubclassSelector]
    private TargetEffectDefinition[] effects;
    
    public TargetAction Compile()
    {
        return new TargetAction(delay, targetSource, directionSelector, effects);
    }
}