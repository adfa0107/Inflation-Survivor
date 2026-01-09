using System;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Data;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Conditions;

[Serializable]
public class CountConditionDefinition : ConditionDefinition
{
    [field: SerializeField] public ScaledValue Count { get; private set; }
    public override ConditionInstance CreateInstance()
    {
        return CountConditionInstance.Get(this);
    }

    public override ConditionData Convert()
    {
        throw new NotImplementedException();
    }
}