using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Casts;

public class SelfCastInstance : CastInstance<SelfCastInstance, SelfCastDefinition>
{
    public override void Setup(SelfCastDefinition data) { }
    public override void Dispose() { }

    public override void Cast(SkillCastModule caster, SkillEffectPackage effectPackage)
    {
        if (caster.combatModule is not null)
        {
            effectPackage.Apply(caster.combatModule);
        }
    }
}