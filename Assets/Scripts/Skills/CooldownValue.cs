using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Skills;

public readonly struct CooldownValue
{
    private readonly IFormula<SkillContext> _valueFormula;

    public CooldownValue(IFormulaDefinition<SkillContext> valueFormula)
    {
        _valueFormula = valueFormula.Build();
    }

    public float Evaluate(SkillContext context)
    {
        return _valueFormula.Evaluate(context) * Mathf.Max(0f, 1 - context.caster.stat[StatType.CooldownReduction]);
    }
}