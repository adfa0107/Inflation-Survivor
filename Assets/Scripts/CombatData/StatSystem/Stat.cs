using System;
using System.Collections.Generic;

namespace InflationSurvivor.CombatData.StatSystem;

public readonly struct Stat
{
    private static readonly int _statCount = Enum.GetValues(typeof(StatType)).Length;

    private readonly float[] _stat;

    public Stat()
    {
        _stat = new float[_statCount];
    }
    
    public float this[StatType type]
    {
        get => _stat[(int)type];
        set => _stat[(int)type] = value;
    }

    public void Reset(IReadOnlyDictionary<StatType, float> stats)
    {
        for (int i = 0; i < _statCount; i++)
        {
            _stat[i] = 0;
        }

        foreach ((StatType type, float value) in stats)
        {
            _stat[(int)type] = value;
        }
    }
}