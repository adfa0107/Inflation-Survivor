using System;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class ConditionData
{
    public abstract ConditionInstance CreateInstance();
}