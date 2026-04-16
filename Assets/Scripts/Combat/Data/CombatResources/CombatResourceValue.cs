using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.Combat.Data.CombatResources;

[Serializable]
public struct CombatResourceValue
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

    public static implicit operator float(CombatResourceValue combatResourceValue)
    {
        return combatResourceValue._value;
    }
}