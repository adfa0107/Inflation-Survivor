using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills.TargetEffects;

public sealed class TargetPosition : TargetEffect
{
    private readonly PositionEffect[] _positionEffects;

    public TargetPosition(PositionEffectDefinition[] positionEffects)
    {
        _positionEffects = new PositionEffect[positionEffects.Length];
        for (int i = 0; i < positionEffects.Length; i++)
        {
            _positionEffects[i] = positionEffects[i].Compile();
        }
    }
    
    public override void ApplyEffect(SkillContext context, Vector3 direction)
    {
        foreach (PositionEffect positionEffect in _positionEffects)
        {
            positionEffect.ApplyEffect(context, context.target.Position, direction);
        }
    }
}