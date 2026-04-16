using System;
using InflationSurvivor.Core.Attributes;
using SerializeReferenceEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace InflationSurvivor.Skills.Primitives.Positions;

[Serializable]
public class PositionActionDefinition
{
    [SerializeField, SerializeReference, SubclassSelector]
    private FormulaDefinition formula;
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionSourceDefinition positionSource;
    [SerializeField, SerializeReference, SubclassSelector]
    private DirectionSelectorDefinition directionSelector;
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionEffectDefinition[] effects;

    public PositionAction Compile() => new PositionAction(formula, positionSource, directionSelector, effects);
}