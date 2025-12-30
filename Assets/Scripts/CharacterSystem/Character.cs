using InflationSurvivor.CombatData;
using InflationSurvivor.Core.Faction;
using InflationSurvivor.SkillSystem;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem
{
    [RequireComponent(typeof(SkillCastModule))]
    public class Character : CombatModule
    {
        public float health;

        private SkillCastModule _skillCastModule;
        
        public SkillCastModule SkillCastModule => _skillCastModule;

        protected void Setup(StatData statData, FactionType faction)
        {
            
        }

        protected override void DamageImplement(float amount)
        {
            health -= amount;
        }

        protected override void HealImplement(float amount)
        {
            health += amount;
        }

        private void Awake()
        {
            _skillCastModule = GetComponent<SkillCastModule>();
        }
    }
    
}
