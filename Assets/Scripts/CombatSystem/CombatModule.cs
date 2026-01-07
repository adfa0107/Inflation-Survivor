using System;
using System.Collections.Generic;
using System.Threading;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.EventSystem;
using InflationSurvivor.StatusEffect;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using EventHandler = InflationSurvivor.EventSystem.EventHandler;

namespace InflationSurvivor.CombatSystem;

public class CombatModule : IDisposable
{
    private static readonly Dictionary<int, CombatModule> _moduleCache = new Dictionary<int, CombatModule>();

    public readonly CancellationToken onDestroyToken;
    
    public readonly EventHandler eventHandler;
    public readonly Stat stat;
    public readonly Resource resource;
    public readonly StatusEffectManager statusEffectManager;

    private readonly int _colliderID;
    public CombatModule([NotNull]EventHandler eventHandler, [NotNull]Collider2D collider, CancellationToken onDestroyToken)
    {
        Assert.IsFalse(_moduleCache.ContainsKey(collider.GetInstanceID()));
        
        this.onDestroyToken = onDestroyToken;
        
        this.eventHandler = eventHandler;
        stat = new Stat();
        resource = new Resource();
        statusEffectManager = new StatusEffectManager(stat, resource);
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

    public void Attack(CombatModule attacker, float amount)
    {
        AttackEvent attackEvent = new AttackEvent
        {
            attacker = attacker,
            target = this,
            damage = amount
        };

        (bool isCancelled, attackEvent) = GameEvent.RaisePrev(attackEvent);

        if (isCancelled)
        {
            return;
        }
        
        resource.Damage(attackEvent.damage, out bool isDead);
        
        GameEvent.RaisePost(attackEvent);
    }

    public void Heal(CombatModule healer, float amount)
    {
        HealEvent healEvent = new HealEvent
        {
            healer = healer,
            target = this,
            healAmount = amount
        };

        (bool isCancelled, healEvent) = GameEvent.RaisePrev(healEvent);

        if (isCancelled)
        {
            return;
        }

        resource.Heal(healEvent.healAmount);
        
        GameEvent.RaisePost(healEvent);
    }

    public void Update(float deltaTime)
    {
        resource.Update(deltaTime);
        statusEffectManager.Update(deltaTime);
    }
}