using System.Collections.Generic;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.SkillSystem;

public sealed class SkillInstance : IInstance<SkillDefinition>
{
    private static readonly InstancePool<SkillInstance, SkillDefinition> _pool = new InstancePool<SkillInstance, SkillDefinition>(100);
    
    private float _skillAvailableTime;
    
    private readonly List<ConditionInstance> _conditions = new List<ConditionInstance>();
    private readonly List<ActionInstance> _actions = new List<ActionInstance>();
    
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public ResourceType CostType { get; private set; }
    public float Cost { get; private set; }
    
    public float DefaultCooldown { get; private set; }

    public float Cooldown
    {
        get => Mathf.Max(_skillAvailableTime - Time.time, 0f);
        set => _skillAvailableTime = Time.time + value;
    }

    public static SkillInstance Get(SkillDefinition data) => _pool.Get(data);
    public void Release() => _pool.Release(this);
        
    public void Setup(SkillDefinition data)
    {
        Name = data.Name;
        Icon = data.Icon;
        
        CostType = data.CostType;
        Cost = data.Cost;
        DefaultCooldown = data.Cooldown;
        _skillAvailableTime = 0f;

        foreach (ConditionData condition in data.Conditions)
        {
            Assert.IsNotNull(condition);
            _conditions.Add(condition.CreateInstance());
        }

        foreach (ActionData action in data.Actions)
        {
            _actions.Add(ActionInstance.Get(action));
        }
    }

    public void Dispose()
    {
        Name = null;
        Icon = null;

        foreach (ConditionInstance condition in _conditions)
        {
            condition.Release();
        }
        _conditions.Clear();

        foreach (ActionInstance action in _actions)
        {
            action.Release();
        }
        _actions.Clear();
    }

    public bool CanUse(SkillCastModule caster)
    {
        foreach (ConditionInstance condition in _conditions)
        {
            if(!condition.CanActivate(caster))
            {
                return false;
            }
        }
        
        return _skillAvailableTime <= Time.time && caster.resource[CostType] >= Cost;
    }

    public void Execute(SkillCastModule caster, GameEvent @event = null)
    {
        bool bIsAllConditionMet = true;
        
        foreach (ConditionInstance condition in _conditions)
        {
            condition.Update(caster);
            bIsAllConditionMet &= condition;
        }
        
        if (!bIsAllConditionMet || _skillAvailableTime > Time.time || caster.resource[CostType] < Cost)
        {
            return;
        }
        
        caster.resource[CostType].Consume(Cost);
        
        foreach (ActionInstance action in _actions)
        {
            _ = action.Execute(caster, @event);
        }

        foreach (ConditionInstance condition in _conditions)
        {
            condition.Deactivate(caster);
        }
        
        Cooldown = DefaultCooldown;
    }
}