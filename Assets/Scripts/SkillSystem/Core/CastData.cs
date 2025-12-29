using System;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class CastData
{
    public abstract CastInstance CreateInstance();
}