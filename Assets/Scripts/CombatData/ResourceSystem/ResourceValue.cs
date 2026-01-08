using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class ResourceValue
{
    public ResourceStat stat = new ResourceStat();

    public float Value { get; private set; }
    public float Loss => stat.max - Value;

    public void Consume(float amount, bool force = false)
    {
        Assert.IsTrue(force || Value >= amount, "If you want forced consumption make force parameter true");
        Value -= amount;
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