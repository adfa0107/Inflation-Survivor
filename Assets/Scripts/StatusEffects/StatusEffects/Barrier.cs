using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Events;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffects.StatusEffects;

public class Barrier : StatusEffect<Barrier, BarrierData>, IInstance<BarrierData>
{
    private float _amount;
    private readonly Action<GameEvent> _onPrevAttackEvent;

    public Barrier()
    {
        _onPrevAttackEvent = OnPrevAttackEvent;
    }
    
    protected override void OnSetup()
    {
        
    }

    protected override void OnDispose()
    {
        
    }

    protected override void OnApply(StatusEffectContext context)
    {
        _amount = data.amount.Evaluate(context);
        context.source.eventHandler.SubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    protected override void OnRemove(CombatModule owner)
    {
        owner.eventHandler.UnsubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    protected override void OnUpdate(CombatModule owner, float tick)
    {
        
    }

    private void OnPrevAttackEvent(GameEvent @event)
    {
        Assert.IsTrue(@event is Prev<AttackEvent>);
        var prevAttackEvent = (Prev<AttackEvent>)@event;
        if (prevAttackEvent.IsCancelled)
        {
            return;
        }

        float reduceAmount = Mathf.Min(prevAttackEvent.data.damage, _amount);
        prevAttackEvent.data.damage -= reduceAmount;
        if (prevAttackEvent.data.damage <= 0)
        {
            prevAttackEvent.Cancel();
        }
        
        _amount -= reduceAmount;
        if (_amount <= 0)
        {
            Release();
        }
    }
}