using System;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public sealed class SelfDefinition : TargetSourceDefinition
{
    public override TargetSource Build()
    {
        return new Self();
    }
}