using System;
using InflationSurvivor.CombatData;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Conditions;

[Serializable]
public class CountConditionData : ConditionData
{
    [field: SerializeField] public ScaledValue Count { get; private set; }
    public override ConditionInstance CreateInstance()
    {
        return CountConditionInstance.Get(this);
    }
}