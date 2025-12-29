using System;
using System.Collections.Generic;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class HealEffect : Effect
{
    [SerializeField] private ScaledValue healAmountScale;
        
    public override void ApplyEffect(SkillContext context, IReadOnlyList<ISkillTarget> targets)
    {
        float healAmount = healAmountScale.GetScaledValue(context.caster);
        foreach (ISkillTarget target in targets)
        {
            target.Heal(healAmount);
        }
    }
}