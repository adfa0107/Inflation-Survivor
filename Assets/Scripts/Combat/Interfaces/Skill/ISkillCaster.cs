using InflationSurvivor.Combat.Handles;

namespace InflationSurvivor.Combat.Interfaces.Skill;

public interface ISkillCaster
{
    public void SetCooldown(string id, float value);
    
    public SkillDisableHandle DisableSkill(ISkill skill);
    public SkillDisableHandle DisableSkillByID(string id);
    public SkillDisableHandle DisableSkillByTag(string tag);
    public SkillDisableHandle DisableSkillByExclusiveTag(string tag);
    
    public void RemoveDisable(SkillDisableHandle handle);
    
    public SkillReplaceHandle ReplaceSkill(ISkill skill, ISkill newSkill, int priority);
    public SkillReplaceHandle ReplaceSkillByID(string id, ISkill newSkill, int priority);
    public SkillReplaceHandle ReplaceSkillByTag(string tag, ISkill newSkill, int priority);
    public SkillReplaceHandle ReplaceSkillByExclusiveTag(string tag, ISkill newSkill, int priority);
    
    public void RemoveReplacement(SkillReplaceHandle handle);
}