using InflationSurvivor.CombatSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

public class PreDefined : LeafTargetSource
{
    public PreDefined(ISkillProcessor<CombatModule> processor) : base(processor) { }
    
    public override void Emit(SkillContext context)
    {
        processor.Process(context, context.target);
    }
}