using System.Collections.Generic;
using adfa.Utility.AI.Steering;
using InflationSurvivor.CombatSystem.StatSystem;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem.Monster;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : StatData
{
    [SerializeField] private WeightedBehavior[] behaviours;
    public IReadOnlyList<WeightedBehavior> Behaviors => behaviours;
}