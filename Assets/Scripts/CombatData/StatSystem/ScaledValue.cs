using System;
using UnityEngine;

namespace InflationSurvivor.CombatData.StatSystem;

[Serializable]
public struct ScaledValue
{
    [Serializable]
    public struct StatScaling
    {
        public StatType statType;
        public float ratio;
    }
        
    [SerializeField] private float baseValue;
    [SerializeField] private StatScaling[] statScalingFactors;

    public float GetScaledValue(Stat stat)
    {
        float scaledValue = baseValue;
        foreach (StatScaling statScaling in statScalingFactors)
        {
            scaledValue += stat[statScaling.statType] * statScaling.ratio;
        }
            
        return scaledValue;
    }

    public int GetScaledValueAsInt(Stat stat)
    {
        return Mathf.FloorToInt(GetScaledValue(stat));
    }
}