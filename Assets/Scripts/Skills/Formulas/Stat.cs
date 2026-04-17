using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;
using UnityEngine.Assertions;

namespace InflationSurvivor.Skills.Formulas;

public class Stat : IFormula<SkillContext>
{
    private readonly ValueSource _source;
    private readonly StatType _statType;
    private readonly float _coefficient;

    public Stat(ValueSource source, StatType statType, float coefficient)
    {
        _source = source;
        _statType = statType;
        _coefficient = coefficient;
    }
    
    public float Evaluate(SkillContext context)
    {
        return _source switch
        {
            ValueSource.Caster => context.caster.stat[_statType] * _coefficient,
            ValueSource.Target => context.target.stat[_statType] * _coefficient,
            _ => 0f
        };
    }
}