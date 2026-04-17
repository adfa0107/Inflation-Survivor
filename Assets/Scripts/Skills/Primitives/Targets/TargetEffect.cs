using InflationSurvivor.Combat.Contexts;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Targets;

public abstract class TargetEffect
{
    public abstract void ApplyEffect(SkillContext context, Vector3 direction);
}