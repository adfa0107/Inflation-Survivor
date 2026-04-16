using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

public abstract class PositionEffect
{
    public abstract void ApplyEffect(SkillContext context, Vector3 position, Vector3 direction);
}