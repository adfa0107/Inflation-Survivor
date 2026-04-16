using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.Formulas;

public sealed class Constant : Formula
{
    private readonly float _value;

    public Constant(float value)
    {
        _value = value;
    }
    
    public override float Evaluate(SkillContext context)
    {
        return _value;
    }
}