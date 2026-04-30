using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Abstractions;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Formulas;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public abstract class StatusEffectDefinition : StatusEffectDefinitionBase
{
    [SerializeField] protected string id;
    [SerializeField] protected string effectName;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int priority;
    
    [SerializeField] protected ExclusiveGroupDefinition exclusiveGroup;
    [SerializeField, SerializeReference, FormulaSelector]
    protected IFormulaDefinition<StatusEffectContext> duration;
    [SerializeField, SerializeReference, FormulaSelector]
    protected IFormulaDefinition<StatusEffectContext> initStack;
    [SerializeField, SerializeReference, FormulaSelector]
    protected IFormulaDefinition<StatusEffectContext> maxStack;
}