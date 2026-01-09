using System.Collections.Immutable;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.Core;
using InflationSurvivor.SkillSystem.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Data;

public record struct SkillData
{
    public string name;
    public Sprite icon;
    public ResourceType costType;
    public ScaledValue cost;
    public ScaledValue cooldown;
    
    public ImmutableArray<ConditionData> conditions;
    public ImmutableArray<ActionData> actions;

    private SkillData(string name, Sprite icon, ResourceType costType, ScaledValue cost, ScaledValue cooldown)
    {
        this.name = name;
        this.icon = icon;
        this.costType = costType;
        this.cost = cost;
        this.cooldown = cooldown;
    }

    public SkillData(SkillDefinition skillDefinition)
        : this(skillDefinition.Name, skillDefinition.Icon, skillDefinition.CostType, skillDefinition.Cost, skillDefinition.Cooldown)
    {
        var conditionsBuilder = ImmutableArray.CreateBuilder<ConditionData>();
        foreach (ConditionDefinition condition in skillDefinition.Conditions)
        {
            conditionsBuilder.Add(condition.Convert());
        }
        conditions = conditionsBuilder.ToImmutable();
        
        var actionsBuilder = ImmutableArray.CreateBuilder<ActionData>();
        foreach (ActionDefinition action in skillDefinition.Actions)
        {
            actionsBuilder.Add(new ActionData(action));
        }
        actions = actionsBuilder.ToImmutable();
    }
    
    [JsonConstructor]
    public SkillData(string name, string path, ResourceType costType, ScaledValue cost, ScaledValue cooldown,
        ConditionData[] conditions, ActionData[] actions)
        : this(name, AssetManager.Get<Sprite>(path), costType, cost, cooldown)
    {
        this.conditions = conditions.ToImmutableArray();
        this.actions = actions.ToImmutableArray();
    }
};