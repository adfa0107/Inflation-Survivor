using System;
using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.StatSystem;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class AttackEffect : SkillEffect
{
    [SerializeField] private ScaledValue damageAmountScale;
        
    public override void ApplyEffect(SkillCastModule caster, GameEvent _, IReadOnlyList<CombatModule> targets)
    {
        float damageAmount = damageAmountScale.GetScaledValue(caster.stat);
        foreach (CombatModule target in targets)
        {
            target.Damage(caster.combatModule, damageAmount);
        }
    }
}