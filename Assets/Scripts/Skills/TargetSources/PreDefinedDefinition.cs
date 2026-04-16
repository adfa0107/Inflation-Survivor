using InflationSurvivor.CombatSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

public class PreDefinedDefinition : TargetSourceDefinition
{
    public override TargetSource Compile(ISkillProcessor<CombatModule> processor)
    {
        return new PreDefined(processor);
    }
}