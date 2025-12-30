using InflationSurvivor.CombatData;
using InflationSurvivor.Core.Faction;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Interfaces;

public interface ISkillEntity : IStatProvider
{
    Transform Transform { get; }
}