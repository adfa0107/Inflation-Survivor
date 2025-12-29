using System;
using System.Collections.Generic;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class AttackEffect : Effect
{
    [SerializeField] private ScaledValue damageAmountScale;
        
    public override void ApplyEffect(SkillContext context, IReadOnlyList<ISkillTarget> targets)
    {
        float damageAmount = damageAmountScale.GetScaledValue(context.caster);
        foreach (ISkillTarget target in targets)
        {
            target.Damage(damageAmount);
        }
    }
}