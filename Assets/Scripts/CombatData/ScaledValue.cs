using System;
using UnityEngine;

namespace InflationSurvivor.CombatData;

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

    public float GetScaledValue(IStatProvider statProvider)
    {
        float scaledValue = baseValue;
        foreach (StatScaling statScaling in statScalingFactors)
        {
            scaledValue += statProvider.Stat[statScaling.statType] * statScaling.ratio;
        }
            
        return scaledValue;
    }

    public int GetScaledValueAsInt(IStatProvider statProvider)
    {
        return Mathf.FloorToInt(GetScaledValue(statProvider));
    }
}