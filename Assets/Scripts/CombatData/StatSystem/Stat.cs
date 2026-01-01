using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData.ResourceSystem;

namespace InflationSurvivor.CombatData.StatSystem;

public class Stat
{
    private static readonly int _statCapacity = Enum.GetValues(typeof(StatType)).Length;
    private static readonly int _maxCostCapacity = Enum.GetValues(typeof(CostType)).Length;
    
    private readonly Dictionary<StatType, float> _stat = new Dictionary<StatType, float>(capacity: _statCapacity);
    private readonly Dictionary<CostType, float> _maxCost = new Dictionary<CostType, float>(capacity: _maxCostCapacity);
    
    public float this[StatType type]
    {
        get => _stat.GetValueOrDefault(type, 0f);
        set => _stat[type] = value;
    }

    public float this[CostType type]
    {
        get => _maxCost.GetValueOrDefault(type, 0f);
        set => _maxCost[type] = value;
    }
}