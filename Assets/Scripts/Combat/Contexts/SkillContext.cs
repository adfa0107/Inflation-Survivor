using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.EventSystem;

namespace InflationSurvivor.Combat.Contexts;

public struct SkillContext
{
    public CombatModule caster;
    public CombatModule target;
    public GameEvent gameEvent;
    public ISkill skill;
}