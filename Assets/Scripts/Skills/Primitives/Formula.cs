using InflationSurvivor.Combat.Contexts;
using UnityEngine;

namespace InflationSurvivor.Skills.Primitives;

public abstract class Formula
{
    public abstract float Evaluate(SkillContext context);

    public int EvaluateInt(SkillContext context)
    {
        return Mathf.FloorToInt(Evaluate(context));
    }
}