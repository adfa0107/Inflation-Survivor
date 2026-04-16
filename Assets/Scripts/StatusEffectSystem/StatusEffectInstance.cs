using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using UnityEngine;

namespace InflationSurvivor.StatusEffect;

public abstract class StatusEffectInstance
{ 
    public abstract int Stack { get; }
    public abstract float Power { get; }
    public abstract float RemainingTime { get; }

    public abstract void Apply(StatusEffectManager manager);
    public abstract void Remove();
    public abstract void Update(float tick);
    public abstract void Refresh(int stack, float duration);
}

public abstract class StatusEffectInstance<TSelf, TData> : StatusEffectInstance, IInstance<TData>
    where TSelf : StatusEffectInstance<TSelf, TData>, new()
    where TData : StatusEffectData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);

    private string _id;
    private string _name;
    private Sprite _icon;
    private float _power;
    
    private float _remainingTime;
    private int _stack;
    private StatusEffectManager _manager;
    
    public sealed override float RemainingTime => _remainingTime;
    public sealed override int Stack => _stack;
    public sealed override float Power => _power;

    public static StatusEffectInstance Get(TData data, int stack, float duration)
    {
        TSelf instance = _pool.Get(data);
        instance._stack = stack;
        instance._remainingTime = duration;
        return instance;
    }

    public void Setup(TData data)
    {
        _id = data.ID;
        _name = data.Name;
        _icon = data.Icon;
        _power = data.Power;
        
        OnSetup(data);
    }

    public void Dispose()
    {
        _name = null;
        _icon = null;
        
        OnDispose();
    }

    public sealed override void Apply(StatusEffectManager manager)
    {
        _manager = manager;
        ApplyEffect(_manager.stat, _manager.combatResource, _manager.eventHandler);
    }

    public sealed override void Remove()
    {
        RemoveEffect(_manager.stat, _manager.combatResource, _manager.eventHandler);
        _manager = null;
        _pool.Release((TSelf)this);
    }

    public sealed override void Update(float tick)
    {
        _remainingTime -= tick;
        OnUpdate(_manager.stat, _manager.combatResource, _manager.eventHandler, tick);
    }

    public sealed override void Refresh(int stack, float duration)
    {
        if (_stack != stack)
        {
            RemoveEffect(_manager.stat, _manager.combatResource, _manager.eventHandler);
            _stack = stack;
            ApplyEffect(_manager.stat, _manager.combatResource, _manager.eventHandler);
        }
        
        _remainingTime = duration;
    }

    protected void Release()
    {
        _manager.RemoveStatusEffect(_id);
    }

    protected abstract void OnSetup(TData data);
    protected abstract void OnDispose();
    
    protected abstract void ApplyEffect(Stat stat, CombatResource combatResource, EventHandler eventHandler);
    protected abstract void RemoveEffect(Stat stat, CombatResource combatResource, EventHandler eventHandler);
    protected abstract void OnUpdate(Stat stat, CombatResource combatResource, EventHandler eventHandler, float tick);
}