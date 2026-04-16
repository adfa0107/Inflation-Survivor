using System;
using System.Collections.Generic;
using System.Threading;
using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.EventSystem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.LowLevelPhysics2D;
using EventHandler = InflationSurvivor.EventSystem.EventHandler;

namespace InflationSurvivor.Combat;

public class CombatModule : IDisposable
{
    private static readonly Dictionary<int, CombatModule> _moduleCache = new Dictionary<int, CombatModule>();

    public readonly CancellationToken onDestroyToken;
    
    public readonly EventHandler eventHandler;
    public readonly Stat stat;
    public readonly CombatResource combatResource;
    public readonly int id;
    
    private readonly Transform _transform;
    
    public Vector3 Position => _transform.position;
    
    public CombatModule([NotNull]EventHandler eventHandler, PhysicsBody body, CancellationToken onDestroyToken)
    {
        Assert.IsFalse(_moduleCache.ContainsKey(body.userData.intValue));
        
        this.onDestroyToken = onDestroyToken;
        
        this.eventHandler = eventHandler;
        stat = new Stat();
        combatResource = new CombatResource();
        id = body.userData.intValue;
        _transform = body.transformObject;
        _moduleCache[id] = this;
    }

    public void Dispose()
    {
        _moduleCache.Remove(id);
    }

    public static bool TryGetModule(PhysicsShape shape, out CombatModule module)
    {
        return _moduleCache.TryGetValue(shape.body.userData.intValue, out module);
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
        
        combatResource[CombatResourceType.Health].Consume(attackEvent.damage, force: true);
        
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

        combatResource[CombatResourceType.Health].Restore(healEvent.healAmount);
        
        GameEvent.RaisePost(healEvent);
    }

    public void Update(float deltaTime)
    {
        combatResource.Update(deltaTime);
    }
}