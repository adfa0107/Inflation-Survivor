using System;
using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.Stat;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Effects;

[Serializable]
public class AttackEffect : SkillEffect
{
    [SerializeField] private ScaledValue damageAmountScale;
        
    public override void ApplyEffect(SkillCastModule caster, GameEventData _, IReadOnlyList<CombatModule> targets)
    {
        float damageAmount = damageAmountScale.GetScaledValue(caster.StatModule);
        foreach (CombatModule target in targets)
        {
            target.Damage(caster.CombatModule, damageAmount);
        }
    }
}