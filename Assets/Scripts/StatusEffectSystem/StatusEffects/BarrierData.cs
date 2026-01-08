using InflationSurvivor.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public class BarrierData : StatusEffectData
{
    [field: SerializeField] public float Amount { get; private set; }
    
    public override float Power => Amount;
    protected override StatusEffectInstance CreateInstance(int stack, float duration)
    {
        return BarrierInstance.Get(this, stack, duration);
    }
}