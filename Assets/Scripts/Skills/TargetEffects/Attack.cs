using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

public sealed class Attack : TargetEffect
{
    private readonly IFormula<SkillContext> _damage;

    public Attack(IFormulaDefinition<SkillContext> damage)
    {
        _damage = damage.Build();
    }
        
    public override void ApplyEffect(SkillContext context, Vector3 direction)
    {
        context.target.Attack(context.caster, _damage.Evaluate(context));
    }
}