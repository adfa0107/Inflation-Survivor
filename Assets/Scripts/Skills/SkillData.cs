using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Interfaces;
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

    public SkillData(string id, string name, Sprite icon, CombatResourceType costType, IFormulaDefinition<SkillContext> cost,
        IFormulaDefinition<SkillContext> cooldown, ConditionDefinition[] conditions, TargetActionDefinition[] targetActions, PositionActionDefinition[] positionActions)
    {
        ID = id;
        this.name = name;
        this.icon = icon;
        this.costType = costType;
        this.cost = cost.Build();
        this.cooldown = new CooldownValue(cooldown);
        this.conditions = new ConditionData[conditions.Length];
        this.targetActions = new TargetAction[targetActions.Length];
        this.positionActions = new PositionAction[positionActions.Length];

        for (int i = 0; i < conditions.Length; i++)
        {
            this.conditions[i] = conditions[i].Build();
        }

        for (int i = 0; i < targetActions.Length; i++)
        {
            this.targetActions[i] = targetActions[i].Build();
        }

        for (int i = 0; i < positionActions.Length; i++)
        {
            this.positionActions[i] = positionActions[i].Build();
        }
        
        DataBase<ISkillData>.Register(this);
    }
    
    public ISkill Create(CombatModule owner)
    {
        return Skill.Get(this, owner);
    }
}