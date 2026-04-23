using System;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public enum BaseSelectPolicy
{
    Old,
    New,
    HigherPriority,
    Stronger,
    Longer
}