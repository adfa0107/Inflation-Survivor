using System;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Conditions;

[Serializable]
public class TimerConditionData : ConditionData
{
    [field: SerializeField] public ScaledValue Cooldown { get; private set; }
        
    public override ConditionInstance CreateInstance()
    {
        return TimerConditionInstance.Get(this);
    }
}