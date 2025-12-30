using System;
using InflationSurvivor.CombatSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

[Serializable]
public class Barrier : StatusEffect
{
    [SerializeField] private float amount;
    
    public override void ApplyEffect(CombatModule target)
    {
        BarrierInstance.Create(target, amount, Duration).Apply();
    }
}