using System.Collections.Generic;
using InflationSurvivor.CombatSystem.ResourceSystem;

namespace InflationSurvivor.CombatSystem.StatSystem;

public class Stat
{
    private readonly Dictionary<StatType, float> _stat = new Dictionary<StatType, float>();
    private readonly Dictionary<CostType, float> _maxCost = new Dictionary<CostType, float>();
        
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