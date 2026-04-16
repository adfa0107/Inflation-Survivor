using InflationSurvivor.Combat.Contexts;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives;

public abstract class DirectionSelector
{
    public abstract Vector3 GetDirection(SkillContext context, Vector3 position);
}