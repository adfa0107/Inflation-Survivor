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
    private readonly Action<GameEventData> _onDamagedDelegate;

    public BarrierInstance()
    {
        _onDamagedDelegate = OnDamaged;
    }

    public static BarrierInstance Create(CombatModule target, float amount, float duration)
    {
        Assert.IsNotNull(target, "target is null");
        
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
        
        _target?.eventModule.SubscribeEvent<Prev<DamageEvent>>(_onDamagedDelegate);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_duration), DelayType.DeltaTime, cancellationToken: tokenSource.Token).SuppressCancellationThrow();
        
        _target?.eventModule.UnsubscribeEvent<Prev<DamageEvent>>(_onDamagedDelegate);
        _target = null;
        _tokenSource.Dispose();
        _tokenSource = null;
        _pool.Release(this);
    }

    private void OnDamaged(GameEventData eventData)
    {
        Assert.IsTrue(eventData is Prev<DamageEvent>, "eventData is not Prev<DamageEvent>");

        if (eventData is not Prev<DamageEvent> prevDamageEvent || 
            prevDamageEvent.isCancelled || 
            ReferenceEquals(prevDamageEvent.data.target, _target))
        {
            return;
        }
        
        float reducedDamage = Mathf.Min(_remainAmount, prevDamageEvent.data.damage);

        prevDamageEvent.data.damage -= reducedDamage;
        _remainAmount -= reducedDamage;
        _isConsumed = Mathf.Approximately(_remainAmount, 0f);

        if (_isConsumed)
        {
            _tokenSource.Cancel();
        }
    }
}