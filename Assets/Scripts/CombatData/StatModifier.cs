using System;

namespace InflationSurvivor.CombatData
{
    [Serializable]
    public struct StatModifier : IEquatable<StatModifier>
    {
        public StatType statType;
        public float value;
        public StatModifierType statModifierType;

        public bool Equals(StatModifier other)
        {
            return statType == other.statType && value.Equals(other.value) && statModifierType == other.statModifierType;
        }

        public override bool Equals(object obj)
        {
            return obj is StatModifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)statType, value, (int)statModifierType);
        }
    }
}