using System;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public enum EffectSelectMethod
{
    Old,
    New,
    HigherPriority,
    Longer
}