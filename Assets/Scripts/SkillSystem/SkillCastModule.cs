using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.CombatSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace InflationSurvivor.SkillSystem;

public class SkillCastModule
{
    public readonly Stat stat;
    public readonly Resource resource;
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
        this.combatModule = combatModule;
        this.transform = transform;
    }
}