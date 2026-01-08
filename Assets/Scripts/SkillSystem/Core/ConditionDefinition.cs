using System;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class ConditionDefinition
{
    public abstract ConditionInstance CreateInstance();
}