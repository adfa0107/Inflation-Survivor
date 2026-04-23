using InflationSurvivor.Combat.Contexts;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

public abstract class PositionSource
{
    public abstract void Connect(ISkillProcessor<Vector3> processor);
    public abstract void Emit(SkillContext context);
}