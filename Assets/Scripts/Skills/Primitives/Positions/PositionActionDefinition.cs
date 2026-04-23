using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

[Serializable]
public class PositionActionDefinition
{
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<SkillContext> formula;
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionSourceDefinition positionSource;
    [SerializeField, SerializeReference, SubclassSelector]
    private DirectionSelectorDefinition directionSelector;
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionEffectDefinition[] effects;

    public PositionAction Build() => new PositionAction(formula, positionSource, directionSelector, effects);
}