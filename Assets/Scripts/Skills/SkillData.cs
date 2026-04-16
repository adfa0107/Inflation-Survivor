using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills;

public sealed class SkillData
{
    public readonly string name;
    public readonly Sprite icon;

    public readonly CombatResourceType costType;
    public readonly Formula cost;
    public readonly Formula cooldown;
    
    public readonly ConditionData[] conditions;
    public readonly TargetAction[] targetActions;
    public readonly PositionAction[] positionActions;

    public SkillData(string name, Sprite icon, CombatResourceType costType, FormulaDefinition cost,
        FormulaDefinition cooldown, ConditionDefinition[] conditions, TargetActionDefinition[] targetActions, PositionActionDefinition[] transformActions)
    {
        this.name = name;
        this.icon = icon;
        this.costType = costType;
        this.cost = cost.Compile();
        this.cooldown = cooldown.Compile();
        this.conditions = new ConditionData[conditions.Length];
        this.targetActions = new TargetAction[targetActions.Length];
        this.positionActions = new PositionAction[transformActions.Length];

        for (int i = 0; i < conditions.Length; i++)
        {
            this.conditions[i] = conditions[i].CreateData();
        }

        for (int i = 0; i < targetActions.Length; i++)
        {
            this.targetActions[i] = targetActions[i].Compile();
        }

        for (int i = 0; i < transformActions.Length; i++)
        {
            this.positionActions[i] = transformActions[i].Compile();
        }
    }
}