using System;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using InflationSurvivor.StatusEffect;
using UnityEngine;
using UnityEngine.Assertions;
using EventHandler = InflationSurvivor.EventSystem.EventHandler;

namespace InflationSurvivor.StatusEffects;

public class BarrierInstance : StatusEffectInstance<BarrierInstance, BarrierData>, IInstance<BarrierData>
{
    private float _amount;
    private readonly Action<GameEvent> _onPrevAttackEvent;

    public BarrierInstance()
    {
        _onPrevAttackEvent = OnPrevAttackEvent;
    }
    
    protected override void OnSetup(BarrierData data)
    {
        _amount = data.Amount;
    }

    protected override void OnDispose()
    {
        
    }

    protected override void ApplyEffect(Stat stat, Resource resource, EventHandler eventHandler)
    {
        eventHandler.SubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    protected override void RemoveEffect(Stat stat, Resource resource, EventHandler eventHandler)
    {
        eventHandler.UnsubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    protected override void OnUpdate(Stat stat, Resource resource, EventHandler eventHandler, float tick)
    {
        
    }

    private void OnPrevAttackEvent(GameEvent @event)
    {
        Assert.IsTrue(@event is Prev<AttackEvent>);
        Prev<AttackEvent> prevDamageEvent = (Prev<AttackEvent>)@event;

        float reduceAmount = Mathf.Min(prevDamageEvent.data.damage, _amount);
        prevDamageEvent.data.damage -= reduceAmount;
        if (prevDamageEvent.data.damage <= 0)
        {
            prevDamageEvent.Cancel();
        }
        
        _amount -= reduceAmount;
        if (_amount <= 0)
        {
            Release();
        }
    }
}