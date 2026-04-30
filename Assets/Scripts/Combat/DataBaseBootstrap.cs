using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.Skill;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.Combat;

public static class DataBaseBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Bootstrap()
    {
        DataBase<ISkillData>.Clear();
        DataBase<IStatusEffectData>.Clear();
        DataBase<IExclusiveGroup>.Clear();
    }
}