using System;
using System.Collections.Generic;
using System.Threading;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.CombatSystem.ResourceSystem;
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
    public readonly Resource resource;

    private readonly int _colliderID;
    private readonly Dictionary<string, (string name, Sprite icon, float power, CancellationTokenSource removeToken)> _statusEffects = new Dictionary<string, (string name, Sprite icon, float power, CancellationTokenSource removeToken)>();

    public CombatModule([NotNull]EventModule eventModule, [NotNull]Collider2D collider, CancellationToken onDestroyToken)
    {
        Assert.IsFalse(_moduleCache.ContainsKey(collider.GetInstanceID()));
        
        this.onDestroyToken = onDestroyToken;
        
        this.eventModule = eventModule;
        stat = new Stat();
        resource = new Resource(stat);
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

    public bool TryAddStatusEffect(string id, (string, Sprite, float power, CancellationTokenSource) effectData)
    {
        if (_statusEffects.TryGetValue(id, out var existing))
        {
            if (existing.power >= effectData.power)
            {
                return false;
            }
            
            existing.removeToken.Cancel();
            _statusEffects[id] = effectData;
            return true;
        }
        
        _statusEffects[id] = effectData;
        return true;
    }

    public void RemoveStatusEffect(string id)
    {
        _statusEffects.Remove(id);
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
        
        resource.Health -= attackEvent.damage;
        
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

        resource.Health += healEvent.healAmount;
        
        GameEvent.RaisePost(healEvent);
    }
}