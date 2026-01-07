using Cysharp.Threading.Tasks;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.Core.Faction;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        public float health;
        
        protected EventModule eventModule;
        protected CombatModule combatModule;
        protected SkillCastModule skillCastModule;
        
        public SkillCastModule SkillCastModule => skillCastModule;
        
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            eventModule = new EventModule();
            combatModule = new CombatModule(eventModule, _collider, this.GetCancellationTokenOnDestroy());
            skillCastModule = new SkillCastModule(combatModule, transform);
        }

        private void OnDisable()
        {
            eventModule.OnDisable();
        }

        private void OnDestroy()
        {
            combatModule.Dispose();
        }
    }
    
}
