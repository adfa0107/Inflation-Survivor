using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

public sealed class Attack : TargetEffect
{
    private readonly Formula _damage;

    public Attack(FormulaDefinition damage)
    {
        _damage = damage.Compile();
    }
        
    public override void ApplyEffect(SkillContext context, Vector3 direction)
    {
        context.target.Attack(context.caster, _damage.Evaluate(context));
    }
}