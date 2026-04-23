using System;
using InflationSurvivor.Combat;

namespace InflationSurvivor.Skills.Primitives.Targets;

[Serializable]
public abstract class TargetSourceDefinition
{
    public abstract TargetSource Build();
}