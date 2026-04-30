using System;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public enum EffectValueSelectMethod
{
    Base,
    Old,
    New,
    Max,
    Min,
    Sum
}