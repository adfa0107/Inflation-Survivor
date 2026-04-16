using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.CombatSystem;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Targets;

public abstract class TargetEffect
{
    public abstract void ApplyEffect(SkillContext context, Vector3 direction);
}