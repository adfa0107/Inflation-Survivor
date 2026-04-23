using System;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

[Serializable]
public abstract class PositionSourceDefinition
{
    public abstract PositionSource Build(ISkillProcessor<Vector3> processor);
}