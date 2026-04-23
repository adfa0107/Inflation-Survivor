using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public class InRangeDefinition : TargetSourceDefinition
{
    [SerializeField, SerializeReference, SubclassSelector]
    private PositionSourceDefinition positionSource;
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<SkillContext> range;
    
    public override TargetSource Build()
        => new InRange(positionSource.Build(), range.Build());
}