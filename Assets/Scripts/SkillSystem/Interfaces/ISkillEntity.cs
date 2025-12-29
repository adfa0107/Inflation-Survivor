using InflationSurvivor.Core.Faction;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Interfaces;

public interface ISkillEntity : IStatProvider
{
    Transform Transform { get; }
}