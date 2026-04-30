using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Abstractions;

public abstract class NoContextRequireFormula : IFormula<SkillContext>, IFormula<StatusEffectContext>
{
    protected abstract float Evaluate();


    public float Evaluate(SkillContext context)
    {
        return Evaluate();
    }

    public float Evaluate(StatusEffectContext context)
    {
        return Evaluate();
    }
}