using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.DirectionSelectors;

public class None : DirectionSelector
{
    public override Vector3 GetDirection(SkillContext context, Vector3 position)
    {
        return Vector3.zero;
    }
}