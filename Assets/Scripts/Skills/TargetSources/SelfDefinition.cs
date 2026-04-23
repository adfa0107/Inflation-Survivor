using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public sealed class SelfDefinition : TargetSourceDefinition
{
    public override TargetSource Build(ISkillProcessor<CombatModule> processor)
    {
        return new Self(processor);
    }
}