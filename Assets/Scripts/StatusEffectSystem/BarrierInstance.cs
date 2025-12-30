using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using InflationSurvivor.EventSystem;
using InflationSurvivor.EventSystem.Data;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffect;

public class BarrierInstance
{
    private static readonly SimplePool<BarrierInstance> _pool = new SimplePool<BarrierInstance>();

    private float _remainAmount;
    private float _duration;
    private bool _isConsumed;
    private IStatusEffectTarget _target;
    private CancellationTokenSource _tokenSource;
    private readonly Action<GameEventData> _onDamagedDelegate;

    public BarrierInstance()
    {
        _onDamagedDelegate = OnDamaged;
    }

    public static BarrierInstance Create(IStatusEffectTarget target, float amount, float duration)
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
        Assert.IsTrue(_target is Component, "_target is not Component");
        
        using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            _tokenSource.Token,
            (_target as Component)?.GetCancellationTokenOnDestroy() ?? default);
        
        _target?.SubscribeEvent<Prev<DamageEvent>>(_onDamagedDelegate);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_duration), DelayType.DeltaTime, cancellationToken: tokenSource.Token).SuppressCancellationThrow();
        
        _target?.UnsubscribeEvent<Prev<DamageEvent>>(_onDamagedDelegate);
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