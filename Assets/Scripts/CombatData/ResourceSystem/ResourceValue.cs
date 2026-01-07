using System;
using UnityEngine;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class ResourceValue
{
    public ResourceStat stat = new ResourceStat();

    public float Value { get; private set; }
    public float Loss => stat.max - Value;

    public bool Consume(float amount)
    {
        if (Value < amount)
        {
            return false;
        }
        
        Value -= amount;
        return true;
    }

    public void Restore(float amount)
    {
        Value = Mathf.Min(Value + amount, stat.max);
    }

    public void Reset(ResourceStat inStat)
    {
        stat = inStat;
        Value = stat.max;
    }
}