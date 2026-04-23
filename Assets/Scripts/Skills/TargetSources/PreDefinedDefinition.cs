using System;
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