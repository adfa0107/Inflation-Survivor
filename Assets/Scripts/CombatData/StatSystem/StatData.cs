using CustomInspector;
using InflationSurvivor.CombatData.ResourceSystem;
using UnityEngine;

namespace InflationSurvivor.CombatData.StatSystem
{
    [CreateAssetMenu(menuName = "Inflation Survivor/Default Character Stat")]
    public class StatData : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<StatType, float> stat;
        [SerializeField] private SerializableDictionary<CostType, float> maxCost;

        public void InitializeStat(Stat targetStat)
        {
            foreach (StatType statType in stat.Keys)
            {
                targetStat[statType] = stat[statType];
            }

            foreach (CostType costType in maxCost.Keys)
            {
                targetStat[costType] = maxCost[costType];
            }
        }
    }
}