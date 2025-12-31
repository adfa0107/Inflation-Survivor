using System;
using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.StatSystem;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class HealEffect : SkillEffect
{
    [SerializeField] private ScaledValue healAmountScale;
        
    public override void ApplyEffect(SkillCastModule caster, GameEvent _, IReadOnlyList<CombatModule> targets)
    {
        float healAmount = healAmountScale.GetScaledValue(caster.stat);
        foreach (CombatModule target in targets)
        {
            target.Heal(caster.combatModule, healAmount);
        }
    }
}