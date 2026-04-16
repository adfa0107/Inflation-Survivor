using InflationSurvivor.Combat;
using InflationSurvivor.EventSystem;
using InflationSurvivor.Skills;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        public float health;
        
        protected EventHandler eventHandler;
        protected CombatModule combatModule;
        protected SkillCastModule skillCastModule;
        
        public SkillCastModule SkillCastModule => skillCastModule;
        public CombatModule CombatModule => combatModule;

        private void Awake()
        {
            eventHandler = new EventHandler();
            //combatModule = new CombatModule(eventHandler, _collider, this.GetCancellationTokenOnDestroy());
            skillCastModule = new SkillCastModule(combatModule, transform);
        }

        private void OnDisable()
        {
            eventHandler.OnDisable();
        }

        private void OnDestroy()
        {
            combatModule.Dispose();
        }
    }
    
}
