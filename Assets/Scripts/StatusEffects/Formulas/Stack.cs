using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.StatusEffects.Formulas;

public class Stack: IFormula<StatusEffectContext>
{
    public Stack() { }
    
    public float Evaluate(StatusEffectContext context)
    {
        return context.stack;
    }
}