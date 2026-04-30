using System.Collections.Generic;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.Skill;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.Skills;

public sealed class Skill : IInstance<SkillData>, ISkill
{
    private static readonly InstancePool<Skill, SkillData> _pool = new InstancePool<Skill, SkillData>(100);
    
    private SkillData _data;
    private CombatModule _owner;
    private float _skillAvailableTime;
    
    private readonly List<Condition> _conditions = new List<Condition>();
    
    
    public float DefaultCooldown { get; private set; }

    public int Level { get; private set; }

    public float Cooldown
    {
        get => Mathf.Max(_skillAvailableTime - Time.time, 0f);
        set => _skillAvailableTime = Time.time + value;
    }

    public bool CanUse { get; private set; }
    public IReadOnlyCollection<string> Tags { get; private set; }

    public static Skill Get(SkillData data, CombatModule owner)
    {
        Skill skill = _pool.Get(data);
        skill._owner = owner;
        return skill;
    }
    public void Release() => _pool.Release(this);
        
    public void Setup(SkillData data)
    {
        _data = data;
        _skillAvailableTime = 0f;
        Level = 1;

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

    public void Execute(CombatModule target)
    {
        SkillContext context = new SkillContext { caster = _owner, skill = this, target = target };
        bool bIsAllConditionMet = true;
        
        foreach (Condition condition in _conditions)
        {
            condition.Update(context);
            bIsAllConditionMet &= condition;
        }
        
        if (!bIsAllConditionMet || _skillAvailableTime > Time.time || context.caster.combatResource[_data.costType] < _data.cost.Evaluate(context))
        {
            return;
        }
        
        context.caster.combatResource[_data.costType].Consume(_data.cost.Evaluate(context));
        
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

    public string ID { get; }
}