using System;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives.Positions;

[Serializable]
public abstract class PositionSourceDefinition
{
    public abstract PositionSource Compile(ISkillProcessor<Vector3> processor);
}