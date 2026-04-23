using InflationSurvivor.Combat;

namespace InflationSurvivor.Skills.Primitives.Targets;

public abstract class LeafTargetSource : TargetSource
{
    protected ISkillProcessor<CombatModule> Processor { get; private set; }

    public override void Connect(ISkillProcessor<CombatModule> processor)
    {
        Processor = processor;
    }
}