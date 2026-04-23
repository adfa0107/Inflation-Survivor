using System;

namespace InflationSurvivor.Skills.Primitives;

[Serializable]
public abstract class ConditionDefinition
{
    public abstract ConditionData Build();
}