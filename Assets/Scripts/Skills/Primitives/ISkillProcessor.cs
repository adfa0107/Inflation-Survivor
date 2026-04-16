using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Skills.Primitives;

public interface ISkillProcessor<in T>
{
    void Process(SkillContext context, T value);
}