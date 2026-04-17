using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Formulas;

public class Constant : IFormula<SkillContext>, IFormula<StatusEffectContext>
{
    private readonly float _value;

    public Constant(float value)
    {
        _value = value;
    }
    
    public float Evaluate(SkillContext context) => _value;

    public float Evaluate(StatusEffectContext context) => _value;
}