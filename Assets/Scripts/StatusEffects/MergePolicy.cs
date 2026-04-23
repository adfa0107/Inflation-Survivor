using System;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public enum MergePolicy
{
    Base,
    Old,
    New,
    Max,
    Min
}