using System;

namespace InflationSurvivor.Combat.Data.Stats;

[Serializable]
public struct StatModifier
{
    public StatType statType;
    public StatModifierType statModifierType;
    public float value;
}