using System;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Conditions;

[Serializable]
public sealed class CountDefinition : ConditionDefinition
{
    [SerializeField, SerializeReference, SubclassSelector] 
    private FormulaDefinition count;
    public override ConditionData CreateData()
    {
        return new CountData(count);
    }
}