using System;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

[Serializable]
public abstract class StatusEffect
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int MaxStack { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }

    public abstract void ApplyEffect(IStatusEffectTarget target);
}