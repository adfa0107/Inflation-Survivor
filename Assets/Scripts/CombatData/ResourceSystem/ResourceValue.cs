using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.CombatData.ResourceSystem;

[Serializable]
public struct ResourceValue
{
    public float max;
    public float fixedRegeneration;
    public float ratioRegeneration;
    private float _value;
    public float Loss => max - _value;
    public float Regeneration => fixedRegeneration + max * ratioRegeneration;

    public void Consume(float amount, bool force = false)
    {
        Assert.IsTrue(force || _value >= amount, "If you want forced consumption make force parameter true");
        _value -= amount;
    }

    public void Restore(float amount)
    {
        _value = Mathf.Min(_value + amount, max);
    }

    public void Reset()
    {
        _value = max;
    }

    public static implicit operator float(ResourceValue resourceValue)
    {
        return resourceValue._value;
    }
}