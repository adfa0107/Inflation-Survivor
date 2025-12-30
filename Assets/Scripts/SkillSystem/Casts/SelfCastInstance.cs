using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Casts;

public class SelfCastInstance : CastInstance<SelfCastInstance, SelfCastData>
{
    public override void Create(SelfCastData data)
    {
        
    }

    public override void Cast(SkillCastModule caster, SkillEffectPackage effectPackage)
    {
        if (caster.CombatModule is not null)
        {
            effectPackage.Apply(caster.CombatModule);
        }
    }
}