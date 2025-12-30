using System;
using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.EventSystem;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class SkillEffect
{
    public abstract void ApplyEffect(SkillCastModule caster, GameEventData eventData, IReadOnlyList<CombatModule> targets);
}