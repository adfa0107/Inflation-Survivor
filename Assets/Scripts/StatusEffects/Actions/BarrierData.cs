using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.StatusEffects.Actions;

public class BarrierData : StatusEffectActionData
{
    public readonly IFormula<StatusEffectContext> amount;

    public BarrierData(IFormula<StatusEffectContext> amount) 
    {
        this.amount = amount;
    }

    public override StatusEffectAction Create()
    {
        return Barrier.Get(this);
    }
}