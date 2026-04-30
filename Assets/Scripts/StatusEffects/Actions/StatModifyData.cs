using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;

namespace InflationSurvivor.StatusEffects.Actions;

public class StatModifyData : StatusEffectActionData
{
    public readonly StatFormulaModifier<StatusEffectContext>[] modifier;
    
    public StatModifyData(StatFormulaModifier<StatusEffectContext>[] modifier)
    {
        this.modifier = modifier;
    }

    public override StatusEffectAction Create()
    {
        return StatModify.Get(this);
    }
}