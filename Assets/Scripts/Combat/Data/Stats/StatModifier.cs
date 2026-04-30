using System;

namespace InflationSurvivor.Combat.Data.Stats;

[Serializable]
public struct StatModifier
{
    public StatType statType;
    public float value;
    public StatModifierType statModifierType;
}