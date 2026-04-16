using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Data.Stats;
using JetBrains.Annotations;
using UnityEngine;

namespace InflationSurvivor.Skills;

public class SkillCastModule
{
    public readonly Stat stat;
    public readonly CombatResource combatResource;
    public readonly CombatModule combatModule;
    public readonly Transform transform;

    public SkillCastModule([NotNull]Stat stat, [NotNull]Transform transform)
    {
        this.stat = stat;
        combatModule = null;
        this.transform = transform;
    }

    public SkillCastModule([NotNull]CombatModule combatModule, [NotNull]Transform transform)
    {
        stat = combatModule.stat;
        combatResource = combatModule.combatResource;
        this.combatModule = combatModule;
        this.transform = transform;
    }
}