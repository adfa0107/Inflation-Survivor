using System.Collections.Generic;
using InflationSurvivor.CombatData.Events;
using InflationSurvivor.EventSystem;
using UnityEngine;

namespace InflationSurvivor.CombatData;

[RequireComponent(typeof(Collider2D), typeof(StatModule), typeof(EventModule))]
public abstract class CombatModule : MonoBehaviour
{
    private static readonly Dictionary<int, CombatModule> _moduleCache = new Dictionary<int, CombatModule>();

    protected Collider2D colliderCache;
    protected StatModule statModule;
    protected EventModule eventModule;
    
    public StatModule StatModule => statModule;
    public EventModule EventModule => eventModule;

    protected abstract void DamageImplement(float amount);
    protected abstract void HealImplement(float amount);

    public void Damage(CombatModule attacker, float amount)
    {
        DamageEvent damageEvent = new DamageEvent
        {
            attacker = attacker,
            target = this,
            damage = amount
        };

        Prev<DamageEvent> prevDamageEvent = new Prev<DamageEvent>
        {
            data = damageEvent, isCancelled = false
        };
        
        eventModule.Raise(prevDamageEvent);

        if (prevDamageEvent.isCancelled)
        {
            return;
        }
        
        DamageImplement(prevDamageEvent.data.damage);

        Post<DamageEvent> postDamageEvent = new Post<DamageEvent>
        {
            Data = prevDamageEvent.data
        };
        
        eventModule.Raise(postDamageEvent);
    }

    public void Heal(CombatModule healer, float amount)
    {
        
    }

    public static bool TryGetModule(Collider2D collider, out CombatModule module)
    {
        return _moduleCache.TryGetValue(collider.GetInstanceID(), out module);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitCache()
    {
        _moduleCache.Clear();
    }
    protected virtual void Awake()
    {
        colliderCache = GetComponent<Collider2D>();
        _moduleCache[colliderCache.GetInstanceID()] = this;
        statModule = GetComponent<StatModule>();
        eventModule = GetComponent<EventModule>();
    }

    protected virtual void OnDestroy()
    {
        _moduleCache.Remove(colliderCache.GetInstanceID());
    }
}