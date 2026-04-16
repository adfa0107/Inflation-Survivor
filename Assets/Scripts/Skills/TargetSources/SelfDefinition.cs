using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public sealed class SelfDefinition : TargetSourceDefinition
{
    public override TargetSource Compile(ISkillProcessor<CombatModule> processor)
    {
        return new Self(processor);
    }
}