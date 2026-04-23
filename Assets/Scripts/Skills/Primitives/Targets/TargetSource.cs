using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Skills.Primitives.Targets;

public abstract class TargetSource
{
    public abstract void Connect(ISkillProcessor<CombatModule> processor);
    public abstract void Emit(SkillContext context);
}