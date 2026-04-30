using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.StatusEffects;

public class BarrierData : StatusEffectData
{
    public readonly IFormula<StatusEffectContext> amount;

    public BarrierData(
        string id, 
        string name, 
        Sprite icon, 
        int priority,
        IExclusiveGroup exclusiveGroup, 
        IFormula<StatusEffectContext> duration,
        IFormula<StatusEffectContext> initStack, 
        IFormula<StatusEffectContext> maxStack, 
        IFormula<StatusEffectContext> amount) 
        : base(id, name, icon, priority, exclusiveGroup, duration, initStack, maxStack)
    {
        this.amount = amount;
    }

    public override IStatusEffect Create()
    {
        return Barrier.Get(this);
    }
}