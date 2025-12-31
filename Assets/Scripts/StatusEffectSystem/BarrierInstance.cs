using System;
using System.Threading;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.Events;
using InflationSurvivor.EventSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffect;

public class BarrierInstance
{
    private static readonly SimplePool<BarrierInstance> _pool = new SimplePool<BarrierInstance>(100);

    private float _remainAmount;
    private float _duration;
    private bool _isConsumed;
    private CombatModule _target;
    private CancellationTokenSource _tokenSource;
    private readonly Action<GameEvent> _onDamagedDelegate;

    public BarrierInstance()
    {
        _onDamagedDelegate = OnDamaged;
    }

    public static BarrierInstance Create(CombatModule target, float amount, float duration)
    {
        Assert.IsNotNull(target);
        
        BarrierInstance instance = _pool.Get();
        instance._remainAmount = amount;
        instance._duration = duration;
        instance._isConsumed = false;
        instance._target = target;
        instance._tokenSource = new CancellationTokenSource();
        
        return instance;
    }

    public async UniTaskVoid Apply()
    {
        using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            _tokenSource.Token,
            _target.onDestroyToken);
        
        _target?.eventModule.SubscribeEvent<Prev<AttackEvent>>(_onDamagedDelegate);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_duration), DelayType.DeltaTime, cancellationToken: tokenSource.Token).SuppressCancellationThrow();
        
        _target?.eventModule.UnsubscribeEvent<Prev<AttackEvent>>(_onDamagedDelegate);
        _target = null;
        _tokenSource.Dispose();
        _tokenSource = null;
        _pool.Release(this);
    }

    private void OnDamaged(GameEvent @event)
    {
        Assert.IsTrue(@event is Prev<AttackEvent>);
        
        Prev<AttackEvent> prevDamageEvent = (Prev<AttackEvent>)@event;

        if (prevDamageEvent.IsCancelled || 
            ReferenceEquals(prevDamageEvent.data.target, _target))
        {
            return;
        }
        
        float reducedDamage = Mathf.Min(_remainAmount, prevDamageEvent.data.damage);

        prevDamageEvent.data.damage -= reducedDamage;
        _remainAmount -= reducedDamage;

        if (prevDamageEvent.data.damage <= 0)
        {
            prevDamageEvent.Cancel();
        }
        
        _isConsumed = Mathf.Approximately(_remainAmount, 0f);

        if (_isConsumed)
        {
            _tokenSource.Cancel();
        }
    }
}