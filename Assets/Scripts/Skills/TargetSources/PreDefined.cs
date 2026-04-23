using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

public class PreDefined : LeafTargetSource
{
    public override void Emit(SkillContext context)
    {
        Processor.Process(context, context.target);
    }
}