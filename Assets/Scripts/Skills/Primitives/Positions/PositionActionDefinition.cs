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

    public PositionAction Build()
    {
        var builtEffects = new PositionEffect[effects.Length];
        
        for (var i = 0; i < effects.Length; i++)
        {
            builtEffects[i] = effects[i].Build();
        }

        return new PositionAction(formula.Build(), positionSource.Build(), directionSelector.Build(), builtEffects);
    }
}