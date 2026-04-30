using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.Actions;

[Serializable]
public class BarrierDefinition : StatusEffectActionDefinition
{
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<StatusEffectContext> amount;
    
    public override StatusEffectActionData Build()
    {
        return new BarrierData(amount.Build());
    }
}