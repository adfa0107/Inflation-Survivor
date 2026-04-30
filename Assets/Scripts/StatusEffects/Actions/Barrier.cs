using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Events;
using InflationSurvivor.EventSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffects.Actions;

public class Barrier : StatusEffectAction<Barrier, BarrierData>
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
        owner.eventHandler.SubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    protected override void OnRemove()
    {
        owner.eventHandler.UnsubscribeEvent<Prev<AttackEvent>>(_onPrevAttackEvent);
    }

    public override void Update(float tick)
    {
        
    }

    private void OnPrevAttackEvent(GameEvent @event)
    {
        Assert.IsTrue(@event is Prev<AttackEvent>);
        var prevAttackEvent = (Prev<AttackEvent>)@event;
        if (prevAttackEvent.IsCancelled || _amount <= 0f || prevAttackEvent.data.target != owner)
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
    }
}