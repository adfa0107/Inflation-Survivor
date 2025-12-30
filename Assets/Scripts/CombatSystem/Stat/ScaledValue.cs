using System;
using UnityEngine;

namespace InflationSurvivor.CombatSystem.Stat;

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

    public float GetScaledValue(StatModule statModule)
    {
        float scaledValue = baseValue;
        foreach (StatScaling statScaling in statScalingFactors)
        {
            scaledValue += statModule.Stat[statScaling.statType] * statScaling.ratio;
        }
            
        return scaledValue;
    }

    public int GetScaledValueAsInt(StatModule statModule)
    {
        return Mathf.FloorToInt(GetScaledValue(statModule));
    }
}