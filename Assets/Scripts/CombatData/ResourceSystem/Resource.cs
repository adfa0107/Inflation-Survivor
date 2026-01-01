using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData.StatSystem;
using UnityEngine;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class Resource
{
    public float maxHealth;
    
    private float _health;
    private readonly Dictionary<CostType, float> _cost = new Dictionary<CostType, float>();
    private readonly Stat _stat;

    public Resource(Stat stat)
    {
        foreach (CostType resourceType in Enum.GetValues(typeof(CostType)))
        {
            _cost[resourceType] = 0f;
        }
        _stat = stat;
    }

    public float Health
    {
        get => _health;
        set => _health = Mathf.Min(value, maxHealth);
    }
    
    public float this[CostType type] => _cost[type];

    public bool Consume(CostType type, float amount)
    {
        bool result = _cost[type] >= amount;
        if (result)
        {
            _cost[type] -= amount;
        }
        
        return result;
    }

    public void Restore(CostType type, float amount)
    {
        _cost[type] += Mathf.Min(amount, _stat[type]);
    }
}