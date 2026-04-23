using System;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;
using UnityEngine.Serialization;

namespace InflationSurvivor.Skills.PositionSources;

[Serializable]
public class TargetPositionDefinition : PositionSourceDefinition
{
    [FormerlySerializedAs("targetSelector")] [SerializeField, SerializeReference, SubclassSelector]
    private TargetSourceDefinition targetSource;
    
    public override PositionSource Build(ISkillProcessor<Vector3> processor)
    {
        return new TargetPosition(targetSource, processor);
    }
}