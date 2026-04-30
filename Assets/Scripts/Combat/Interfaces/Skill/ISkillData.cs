namespace InflationSurvivor.Combat.Interfaces.Skill;

public interface ISkillData : IHasID
{
    public ISkill Create(CombatModule owner);
}