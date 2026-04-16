using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Contexts;

public struct SkillContext
{
    public CombatModule caster;
    public CombatModule target;
    public ISkill skill;
}