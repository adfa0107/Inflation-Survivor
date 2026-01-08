using System;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Casts;

[Serializable]
public class SelfCastDefinition : CastDefinition
{
    public override CastInstance CreateInstance()
    {
        return SelfCastInstance.Get(this);
    }
}