using System;
using System.Threading;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using InflationSurvivor.CombatSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffect;

public abstract class StatusEffectInstance
{
    protected abstract UniTaskVoid AddEffect(CombatModule target);
}

public abstract class StatusEffectInstance<TSelf, TData> : StatusEffectInstance, IInstance<TData>
    where TSelf : StatusEffectInstance<TSelf, TData>, new()
    where TData : StatusEffectData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);

    private string _id;
    private string _name;
    private Sprite _icon;

    protected CancellationTokenSource childCancelToken;
    private CancellationTokenSource _targetCancelToken;
    
    private float _duration;
    
    public static StatusEffectInstance Get(TData data) => _pool.Get(data);
    
    protected CombatModule Target { get; private set; }
    protected abstract float EffectPower { get; }

    public virtual void Setup(TData data)
    {
        _id = data.ID;
        _name = data.Name;
        _icon = data.Icon;
        
        childCancelToken = new CancellationTokenSource();
        _targetCancelToken = new CancellationTokenSource();
        
        _duration = data.Duration;
        
        OnSetup(data);
    }
    protected abstract void OnSetup(TData data);

    public virtual void Reset()
    {
        _id = null;
        _name = null;
        _icon = null;
        
        childCancelToken.Dispose();
        childCancelToken = null;
        _targetCancelToken.Dispose();
        _targetCancelToken = null;
        
        OnReset();
    }

    protected abstract void OnReset();

    protected sealed override async UniTaskVoid AddEffect(CombatModule target)
    {
        Assert.IsNull(Target);
        
        Target = target;
        if (string.IsNullOrEmpty(_id))
        {
            
        }
        else
        {
            if (!Target.TryAddStatusEffect(_id, (_name, _icon, EffectPower, _targetCancelToken)))
            {
                return;
            }
        }
        
        ApplyEffect();
        int index = await UniTask.WhenAny(
                UniTask.Delay(TimeSpan.FromSeconds(_duration)),
                UniTask.WaitUntilCanceled(childCancelToken.Token),
                UniTask.WaitUntilCanceled(_targetCancelToken.Token),
                UniTask.WaitUntilCanceled(Target.onDestroyToken)
        );
        RemoveEffect();
        if (index != 2)
        {
            Target.RemoveStatusEffect(_id);
        }
        Target = null;
        
        _pool.Release((TSelf)this);
    }

    protected abstract void ApplyEffect();
    protected abstract void RemoveEffect();
}