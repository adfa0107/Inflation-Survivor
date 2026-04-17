using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
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
    
    public override TargetSource Compile(ISkillProcessor<CombatModule> processor)
        => new InRange(positionSource, range, processor);
}