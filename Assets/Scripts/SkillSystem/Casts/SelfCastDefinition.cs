using System;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Data;

namespace InflationSurvivor.SkillSystem.Casts;

[Serializable]
public class SelfCastDefinition : CastDefinition
{
    public override CastInstance CreateInstance()
    {
        return SelfCastInstance.Get(this);
    }

    public override CastData GetData()
    {
        throw new NotImplementedException();
    }
}