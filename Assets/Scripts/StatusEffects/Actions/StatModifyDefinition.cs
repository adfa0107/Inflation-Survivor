using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.Actions;

[Serializable]
public class StatModifyDefinition : StatusEffectActionDefinition
{
    [SerializeField] private StatFormulaModifierDefinition<StatusEffectContext>[] modifiers;
        
    public override StatusEffectActionData Build()
    {
        StatFormulaModifier<StatusEffectContext>[] builtModifiers = new StatFormulaModifier<StatusEffectContext>[modifiers.Length];
        for (int i = 0; i < modifiers.Length; i++)
        {
            builtModifiers[i] = modifiers[i].Build();
        }
            
        return new StatModifyData(builtModifiers);
    }
}