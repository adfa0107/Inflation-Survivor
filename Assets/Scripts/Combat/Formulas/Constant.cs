using InflationSurvivor.Combat.Abstractions;

namespace InflationSurvivor.Combat.Formulas;

public class Constant : NoContextRequireFormula
{
    private readonly float _value;

    public Constant(float value)
    {
        _value = value;
    }

    protected override float Evaluate() => _value;
}