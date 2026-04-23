using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

public sealed class Heal : TargetEffect
{
    private readonly IFormula<SkillContext> _heal;

    public Heal(IFormula<SkillContext> heal)
    {
        _heal = heal;
    }
        
    public override void ApplyEffect(SkillContext context, Vector3 direction)
    {
        context.target.Heal(context.caster, _heal.Evaluate(context));
    }
}