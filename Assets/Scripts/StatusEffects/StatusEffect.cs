using System.Collections.Generic;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using InflationSurvivor.Core.ObjectPool;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public sealed class StatusEffect : IStatusEffect, IInstance<StatusEffectData>
{
    private static readonly InstancePool<StatusEffect, StatusEffectData> _pool =
        new InstancePool<StatusEffect, StatusEffectData>(100);   
    
    private StatusEffectData _data;
    private CombatModule _owner;
    private readonly List<StatusEffectAction> _actions = new List<StatusEffectAction>();
    
    public static StatusEffect Get(StatusEffectData data) => _pool.Get(data);

    public void Setup(StatusEffectData data)
    {
        _data = data;
        foreach (StatusEffectActionData actionData in data.actions)
        {
            _actions.Add(actionData.Create());
        }
    }

    public void Dispose()
    {
        foreach (StatusEffectAction action in _actions)
        {
            action.Release();
        }
        _actions.Clear();
        _data = null;
    }

    public int Stack { get; private set; }
    public float RemainingTime { get; private set; }
    public IStatusEffectData Data => _data;
    
    public void Apply(StatusEffectContext context)
    {
        _owner = context.target;
        Stack = Mathf.FloorToInt(_data.InitStack.Evaluate(context));
        RemainingTime = _data.Duration.Evaluate(context);
        context.stack = Stack;
        foreach (StatusEffectAction action in _actions)
        {
            action.Apply(context);
        }
    }

    public void Refresh(StatusEffectContext context, int stack, float duration)
    {
        if (Stack != stack)
        {
            foreach (StatusEffectAction action in _actions)
            {
                action.Remove();
            }
            Stack = stack;
            context.stack = Stack;
            foreach (StatusEffectAction action in _actions)
            {
                action.Apply(context);
            }
        }
        
        RemainingTime = duration;
    }

    public void Remove()
    {
        if (_owner != null)
        {
            foreach (StatusEffectAction action in _actions)
            {
                action.Remove();
            }
            _owner = null;
        }
        _pool.Release(this);
    }

    public void Update(float deltaTime)
    {
        RemainingTime -= deltaTime;
        foreach (StatusEffectAction action in _actions)
        {
            action.Update(deltaTime);
        }
    }
}