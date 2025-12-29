using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;

namespace InflationSurvivor.SkillSystem.Casts;

public class SelfCastInstance : CastInstance<SelfCastInstance, SelfCastData>
{
    public override void Create(SelfCastData data)
    {
        
    }

    public override void Cast(SkillContext context, EffectPackage effectPackage)
    {
        if (context.caster is ISkillTarget target)
        {
            effectPackage.Apply(target);
        }
    }
}