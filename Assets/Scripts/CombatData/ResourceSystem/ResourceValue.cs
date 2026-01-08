using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class ResourceValue
{
    public ResourceStat stat;
    private float _value;
    public float Loss => stat.max - _value;

    public void Consume(float amount, bool force = false)
    {
        Assert.IsTrue(force || _value >= amount, "If you want forced consumption make force parameter true");
        _value -= amount;
    }

    public void Restore(float amount)
    {
        _value = Mathf.Min(_value + amount, stat.max);
    }

    public void Reset(ResourceStat inStat)
    {
        stat = inStat;
        _value = stat.max;
    }

    public static implicit operator float(ResourceValue resourceValue)
    {
        Assert.IsNotNull(resourceValue);
        return resourceValue._value;
    }
}