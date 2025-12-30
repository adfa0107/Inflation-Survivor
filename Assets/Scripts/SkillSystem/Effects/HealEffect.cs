using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class HealEffect : SkillEffect
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