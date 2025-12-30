using System;
using System.Collections.Generic;
using System.Threading;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.CombatSystem.StatSystem;
using InflationSurvivor.EventSystem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.CombatSystem;

public class CombatModule : IDisposable
{
    private static readonly Dictionary<int, CombatModule> _moduleCache = new Dictionary<int, CombatModule>();

    public readonly CancellationToken onDestroyToken;
    
    public readonly EventModule eventModule;
    public readonly Stat stat;

    private readonly int _colliderID;

    public CombatModule([NotNull]EventModule eventModule, [NotNull]Collider2D collider, CancellationToken onDestroyToken)
    {
        Assert.IsFalse(_moduleCache.ContainsKey(collider.GetInstanceID()));
        
        this.onDestroyToken = onDestroyToken;
        
        this.eventModule = eventModule;
        stat = new Stat();
        _colliderID = collider.GetInstanceID();
        _moduleCache[_colliderID] = this;
    }

    public void Dispose()
    {
        _moduleCache.Remove(_colliderID);
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
        
        Post<DamageEvent> postDamageEvent = new Post<DamageEvent>
        {
            Data = prevDamageEvent.data
        };
            
        eventModule.Raise(postDamageEvent);
    }

    public void Heal(CombatModule healer, float amount)
    {
        HealEvent healEvent = new HealEvent
        {
            healer = healer,
            target = this,
            healAmount = amount
        };

        Prev<HealEvent> prevHealEvent = new Prev<HealEvent>
        {
            data = healEvent,
            isCancelled = false
        };
        eventModule.Raise(prevHealEvent);

        if (prevHealEvent.isCancelled)
        {
            return;
        }

        Post<HealEvent> postHealEvent = new Post<HealEvent>
        {
            Data = prevHealEvent.data
        };
        eventModule.Raise(postHealEvent);
    }
}