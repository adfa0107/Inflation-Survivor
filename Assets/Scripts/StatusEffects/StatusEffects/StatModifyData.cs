using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.StatusEffects;

public class StatModifyData : StatusEffectData
{
    public readonly StatFormulaModifier<StatusEffectContext>[] modifier;
    
    public StatModifyData(
        string id, 
        string name, 
        Sprite icon, 
        int priority, 
        IExclusiveGroup exclusiveGroup, 
        IFormula<StatusEffectContext> duration, 
        IFormula<StatusEffectContext> initStack, 
        IFormula<StatusEffectContext> maxStack,
        StatFormulaModifier<StatusEffectContext>[] modifier) : base(id, name, icon, priority, exclusiveGroup, duration, initStack, maxStack)
    {
        this.modifier = modifier;
    }

    public override IStatusEffect Create()
    {
        return StatModify.Get(this);
    }
}