using System;
using InflationSurvivor.SkillSystem.Data;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class CastDefinition
{
    public abstract CastInstance CreateInstance();
    public abstract CastData GetData();
}