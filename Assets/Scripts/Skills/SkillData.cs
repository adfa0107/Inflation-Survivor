using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.Skill;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills;

public sealed class SkillData : ISkillData
{
    public readonly string name;
    public readonly Sprite icon;

    public readonly CombatResourceType costType;
    public readonly IFormula<SkillContext> cost;
    public readonly CooldownValue cooldown;
    
    public readonly ConditionData[] conditions;
    public readonly TargetAction[] targetActions;
    public readonly PositionAction[] positionActions;
    
    public string ID { get; }

    public SkillData(string id, string name, Sprite icon, CombatResourceType costType, IFormula<SkillContext> cost,
        IFormula<SkillContext> cooldown, ConditionData[] conditions, TargetAction[] targetActions, PositionAction[] positionActions)
    {
        ID = id;
        this.name = name;
        this.icon = icon;
        this.costType = costType;
        this.cost = cost;
        this.cooldown = new CooldownValue(cooldown);
        this.conditions = conditions;
        this.targetActions = targetActions;
        this.positionActions = positionActions;
        
        DataBase<ISkillData>.Register(this);
    }
    
    public ISkill Create(CombatModule owner)
    {
        return Skill.Get(this, owner);
    }
}