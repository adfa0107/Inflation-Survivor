using UnityEngine;

namespace InflationSurvivor.StatSystem
{
    [CreateAssetMenu(menuName = "Inflation Survivor/Default Character Stat")]
    public class StatData : ScriptableObject
    {
        [SerializeField] private float attackDamage;
        [SerializeField] private float defense;
        [SerializeField] private float maxHealth;
        [SerializeField] private float speed;

        public Stat CreateStat()
        {
            Stat stat = new()
            {
                [StatType.AttackDamage] = attackDamage,
                [StatType.Defense] = defense,
                [StatType.Speed] = speed,
                [StatType.MaxHealth] = maxHealth
            };
            return stat;
        }
    }
}