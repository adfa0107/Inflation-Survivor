using System;
using JetBrains.Annotations;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

[Serializable]
public class Barrier : StatusEffect
{
    [SerializeField] private float amount;
    
    public override void ApplyEffect([NotNull]IStatusEffectTarget target)
    {
        BarrierInstance.Create(target, amount, Duration).Apply();
    }
}