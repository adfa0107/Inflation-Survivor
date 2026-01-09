using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Data;

public abstract record CastData()
{
    public abstract CastInstance CreateInstance();
};