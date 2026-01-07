using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData.StatSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class Resource
{
    private static readonly int _costCount = Enum.GetValues(typeof(ResourceType)).Length;
    
    private readonly ResourceValue[] _resources = new ResourceValue[_costCount];
    private readonly ResourceValue _healthValue;
    
    public ResourceStat HealthStat => _healthValue.stat;
    public float Health => _healthValue.Value;
    public ResourceValue this[ResourceType resourceType]
    {
        get
        {
            Assert.IsTrue(resourceType != ResourceType.Health, "Health access must be use HealthStat, Health, Damage, Heal");
            return _resources[(int)resourceType];
        }
    }

    public Resource()
    {
        for (int i = 0; i < _costCount; i++)
        {
            _resources[i] = new ResourceValue();
        }
        _healthValue = _resources[(int)ResourceType.Health];
    }

    public void Damage(float amount, out bool isDead)
    {
        _healthValue.Consume(Mathf.Min(amount, _healthValue.Value));
        isDead = _healthValue.Value == 0;
    }

    public void Heal(float amount)
    {
        _healthValue.Restore(amount);
    }

    public void Reset(IReadOnlyDictionary<ResourceType, ResourceStat> resourceStats)
    {
        foreach (ResourceValue resourceValue in _resources)
        {
            resourceValue.Reset(default);
        }

        foreach ((ResourceType type, ResourceStat stat) in resourceStats)
        {
            _resources[(int)type].Reset(stat);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (ResourceValue resource in _resources)
        {
            resource.Restore(resource.stat.Regeneration * deltaTime);
        }
    }
}