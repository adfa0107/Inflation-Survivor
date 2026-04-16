using InflationSurvivor.CombatSystem;

namespace InflationSurvivor.Skills.Primitives.Targets;

public abstract class LeafTargetSource : TargetSource
{
    protected readonly ISkillProcessor<CombatModule> processor;

    protected LeafTargetSource(ISkillProcessor<CombatModule> processor)
    {
        this.processor = processor;
    }
}