using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.StatusEffects.Formulas;

[Serializable]
public class StackDefinition: IFormulaDefinition<StatusEffectContext>
{
    public IFormula<StatusEffectContext> Build()
    {
        return new Stack();
    }
}