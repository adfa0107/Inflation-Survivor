using System;

namespace InflationSurvivor.Skills.Primitives.Targets;

[Serializable]
public abstract class TargetEffectDefinition
{
    public abstract TargetEffect Compile();
}