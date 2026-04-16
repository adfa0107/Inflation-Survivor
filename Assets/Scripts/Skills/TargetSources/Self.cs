using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

public sealed class Self : LeafTargetSource
{
    public Self(ISkillProcessor<CombatModule> processor) : base(processor) { }
    
    public override void Emit(SkillContext context)
    {
        processor.Process(context, context.caster);
    }
}