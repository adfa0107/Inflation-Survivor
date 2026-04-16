using InflationSurvivor.CombatSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

public sealed class Heal : TargetEffect
{
    private readonly Formula _heal;

    public Heal(FormulaDefinition heal)
    {
        _heal = heal.Compile();
    }
        
    public override void ApplyEffect(SkillContext context, Vector3 direction)
    {
        context.target.Heal(context.caster.combatModule, _heal.Evaluate(context));
    }
}