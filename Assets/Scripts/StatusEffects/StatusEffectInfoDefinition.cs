using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public class StatusEffectInfoDefinition
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private int priority;
    
    [SerializeField] private ExclusiveGroupDefinition exclusiveGroup;
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<StatusEffectContext> duration;
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<StatusEffectContext> initStack;
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<StatusEffectContext> maxStack;

    public StatusEffectInfo Build()
    {
        return new StatusEffectInfo(id, name, icon, priority, exclusiveGroup.Build(), duration.Build(), initStack.Build(), maxStack.Build());
    }
}