using System.Collections.Generic;

namespace InflationSurvivor.CombatSystem.Stat;

public class Stat
{
    private readonly Dictionary<StatType, float> _stat = new Dictionary<StatType, float>();
        
    public float this[StatType type]
    {
        get => _stat.GetValueOrDefault(type, 0f);
        set => _stat[type] = value;
    }
}