using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using InflationSurvivor.Core.ObjectPool;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public abstract class StatusEffect<TSelf, TData> : IStatusEffect, IInstance<TData>
    where TSelf : StatusEffect<TSelf, TData>, new()
    where TData : StatusEffectData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);

    protected TData data;

    private CombatModule _owner;

    public float RemainingTime { get; private set; }
    public int Stack { get; private set; }
    public IStatusEffectData Data => data;
    
    
    public static IStatusEffect Get(TData data) => _pool.Get(data);
    protected void Release() => _pool.Release((TSelf)this);

    public void Setup(TData data)
    {
        this.data = data;
        
        OnSetup();
    }

    public void Dispose()
    {
        data = null;
        
        OnDispose();
    }

    public void Apply(StatusEffectContext context)
    {
        _owner = context.target;
        Stack = Mathf.FloorToInt(data.InitStack.Evaluate(context));
        RemainingTime = data.Duration.Evaluate(context);
        OnApply(context);
    }

    public void Remove()
    {
        if (_owner != null)
        {
            OnRemove(_owner);
            _owner = null;
        }
        _pool.Release((TSelf)this);
    }

    public void Update(float tick)
    {
        RemainingTime -= tick;
        OnUpdate(_owner, tick);
    }

    public void Refresh(StatusEffectContext context, int stack, float duration)
    {
        if (Stack != stack)
        {
            OnRemove(_owner);
            Stack = stack;
            OnApply(context);
        }
        
        RemainingTime = duration;
    }

    protected abstract void OnSetup();
    protected abstract void OnDispose();
    
    protected abstract void OnApply(StatusEffectContext context);
    protected abstract void OnRemove(CombatModule owner);
    protected abstract void OnUpdate(CombatModule owner, float tick);
}