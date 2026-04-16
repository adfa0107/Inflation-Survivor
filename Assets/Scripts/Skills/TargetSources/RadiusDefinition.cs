using System;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.Serialization;

namespace InflationSurvivor.Skills.TargetSources;

[Serializable]
public class RadiusDefinition : TargetSourceDefinition
{
    [FormerlySerializedAs("positionSelector")] [SerializeField, SerializeReference, SubclassSelector]
    private PositionSourceDefinition positionSource;
    [SerializeField, SerializeReference, SubclassSelector]
    private FormulaDefinition minRadius;
    [SerializeField, SerializeReference, SubclassSelector]
    private FormulaDefinition maxRadius;
    
    public override TargetSource Compile(ISkillProcessor<CombatModule> processor)
    {
        return new Radius(positionSource, minRadius, maxRadius, processor);
    }
}