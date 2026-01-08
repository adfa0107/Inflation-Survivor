using System;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class CastDefinition
{
    public abstract CastInstance CreateInstance();
}