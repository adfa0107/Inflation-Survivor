using System;
using InflationSurvivor.CombatSystem;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

[Serializable]
public abstract class StatusEffectData
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }

    public abstract void ApplyEffect(CombatModule target);
}