using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Skills.Primitives.Positions;

public abstract class PositionSource
{
    public abstract void Emit(SkillContext context);
}