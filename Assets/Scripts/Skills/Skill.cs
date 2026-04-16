using System.Collections.Generic;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.Skills;

public sealed class Skill : IInstance<SkillData>
{
    private static readonly InstancePool<Skill, SkillData> _pool = new InstancePool<Skill, SkillData>(100);
    
    private SkillData _data;
    private float _skillAvailableTime;
    
    private readonly List<Condition> _conditions = new List<Condition>();
    
    
    public float DefaultCooldown { get; private set; }

    public float Cooldown
    {
        get => Mathf.Max(_skillAvailableTime - Time.time, 0f);
        set => _skillAvailableTime = Time.time + value;
    }

    public static Skill Get(SkillData data) => _pool.Get(data);
    public void Release() => _pool.Release(this);
        
    public void Setup(SkillData data)
    {
        _data = data;
        _skillAvailableTime = 0f;

        foreach (ConditionData condition in _data.conditions)
        {
            Assert.IsNotNull(condition);
            _conditions.Add(condition.Create());
        }
    }

    public void Dispose()
    {
        _data = null;
        
        foreach (Condition condition in _conditions)
        {
            condition.Release();
        }
        _conditions.Clear();
    }

    public bool CanUse(SkillContext context)
    {
        foreach (Condition condition in _conditions)
        {
            if(!condition.CanActivate(context))
            {
                return false;
            }
        }
        
        return _skillAvailableTime <= Time.time && context.caster.combatModule.resource[_data.costType] >= _data.cost.Evaluate(context);
    }

    public void Execute(SkillContext context)
    {
        bool bIsAllConditionMet = true;
        
        foreach (Condition condition in _conditions)
        {
            condition.Update(context);
            bIsAllConditionMet &= condition;
        }
        
        if (!bIsAllConditionMet || _skillAvailableTime > Time.time || context.caster.combatModule.resource[_data.costType] < _data.cost.Evaluate(context))
        {
            return;
        }
        
        context.caster.combatModule.resource[_data.costType].Consume(_data.cost.Evaluate(context));
        
        foreach (TargetAction targetAction in _data.targetActions)
        {
            _ = targetAction.Execute(context);
        }

        foreach (PositionAction transformAction in _data.positionActions)
        {
            _ = transformAction.Execute(context);
        }

        foreach (Condition condition in _conditions)
        {
            condition.Deactivate(context);
        }
        
        Cooldown = DefaultCooldown;
    }
}