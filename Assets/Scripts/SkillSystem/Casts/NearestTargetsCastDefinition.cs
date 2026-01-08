using System;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Casts;

[Flags]
public enum TargetFaction
{
    Ally = 1,
    Enemy
}
    
[Serializable]
public class NearestTargetsCastDefinition : CastDefinition
{
    [field: SerializeField] public bool bIsIncludeSelf { get; private set; }
    
    [field: SerializeField] public ScaledValue Angle { get; private set; }
    [field: SerializeField] public ScaledValue MinRadius { get; private set; }
    [field: SerializeField] public ScaledValue MaxRadius { get; private set; }
    [field: SerializeField] public ScaledValue MaxTargetCount { get; private set; }
    
    [field: SerializeField] public TargetFaction TargetFaction { get; private set; } = TargetFaction.Enemy;
    public override CastInstance CreateInstance()
    {
        return NearestTargetsCastInstance.Get(this);
    }
}