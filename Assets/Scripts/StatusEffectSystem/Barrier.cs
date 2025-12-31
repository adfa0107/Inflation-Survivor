using System;
using InflationSurvivor.CombatSystem;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

[Serializable]
public class Barrier : StatusEffectData
{
    [SerializeField] private float amount;
    
    public override void ApplyEffect(CombatModule target)
    {
        BarrierInstance.Create(target, amount, Duration).Apply();
    }
}