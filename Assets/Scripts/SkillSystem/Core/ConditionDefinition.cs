using System;
using InflationSurvivor.SkillSystem.Data;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class ConditionDefinition
{
    public abstract ConditionInstance CreateInstance();
    public abstract ConditionData Convert();
}