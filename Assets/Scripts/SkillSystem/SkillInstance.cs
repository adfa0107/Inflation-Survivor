using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.SkillSystem;

public sealed class SkillInstance : IInstance<SkillData>
{
    private static readonly InstancePool<SkillInstance, SkillData> _pool = new(100);
    private readonly List<ComponentInstance> _components = new List<ComponentInstance>();
    
    public float SkillAvailableTime { get; private set; }
    public float Cooldown { get; private set; }

    public static SkillInstance Get(SkillData data) => _pool.Get(data);
    public void Release() => _pool.Release(this);
        
    public void Setup(SkillData data)
    {
        Cooldown = data.Cooldown;
        
        foreach (ComponentData component in data.Components)
        {
            Assert.IsNotNull(component);
            _components.Add(ComponentInstance.Get(component));
        }
    }

    public void Reset()
    {
        foreach (ComponentInstance component in _components)
        {
            component.Release();
        }
        _components.Clear();
    }

    public void Execute(SkillCastModule caster, GameEventData eventData = null)
    {
        if (SkillAvailableTime > Time.time)
        {
            return;
        }
        
        foreach (ComponentInstance component in _components)
        {
            component.Execute(caster, eventData);
        }
        
        SetCooldown(Cooldown);
    }

    public void SetCooldown(float cooldown)
    {
        SkillAvailableTime = Time.time + cooldown;
    }
}