using Cysharp.Threading.Tasks;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem;
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
        
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            eventHandler = new EventHandler();
            combatModule = new CombatModule(eventHandler, _collider, this.GetCancellationTokenOnDestroy());
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
