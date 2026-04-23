using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Targets;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public class PreDefinedDefinition : TargetSourceDefinition
{
    public override TargetSource Build()
    {
        return new PreDefined();
    }
}