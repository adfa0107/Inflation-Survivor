using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

public abstract class LeafPositionSource : PositionSource
{
    protected ISkillProcessor<Vector3> processor;
    
    protected LeafPositionSource(ISkillProcessor<Vector3> processor) => this.processor = processor;
}