using System;
using System.Collections;
using InflationSurvivor.CombatData;
using InflationSurvivor.Core.Faction;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem
{
    public class Character : MonoBehaviour, ISkillCaster, ISkillTarget, IStatusEffectTarget
    {
        public float health;
        
        public Stat Stat { get; private set; } = new Stat();
        public FactionType Faction { get; private set; }
        public Vector2 Position => transform.position;
        public Transform Transform => transform;

        private Collider2D _collider;

        public void Heal(float amount)
        {
            health += amount;
        }

        public void Damage(float amount)
        {
            health -= amount;
        }

        protected void Setup(StatData statData, FactionType faction)
        {
            Stat = statData.CreateStat();
            Faction = faction;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            SkillTargetCache.Register(_collider, this);
        }

        private void OnDestroy()
        {
            SkillTargetCache.Unregister(_collider);
        }

        public void SubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData
        {
            throw new NotImplementedException();
        }

        public void Raise<TEventData>(TEventData eventData) where TEventData : GameEventData
        {
            throw new NotImplementedException();
        }
    }
    
}
