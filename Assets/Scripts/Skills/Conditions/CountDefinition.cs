using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Conditions;

[Serializable]
public sealed class CountDefinition : ConditionDefinition
{
    [SerializeField, SerializeReference, FormulaSelector] 
    private IFormulaDefinition<SkillContext> count;
    public override ConditionData Build()
    {
        return new CountData(count);
    }
}