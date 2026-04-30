using System.Collections.Generic;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public class StatusEffectManager : IStatusEffectManager
{
    private readonly CombatModule _owner;
    private readonly Dictionary<string, IStatusEffect> _effects = new();

    public StatusEffectManager(CombatModule owner)
    {
        _owner = owner;
    }
    
    public bool Has(string id)
    {
        return _effects.ContainsKey(id);
    }

    public void Add(IStatusEffectData effectData, CombatModule source)
    {
        var context = new StatusEffectContext {target = _owner, source = source};
        string id = effectData.ExclusiveGroup?.ID ?? effectData.ID;
        IStatusEffect newEffect = effectData.Create();

        if (!_effects.TryGetValue(id, out IStatusEffect oldEffect))
        {
            newEffect.Apply(context);
            _effects.Add(id, newEffect);
            return;
        }
        
        float duration = effectData.Duration.Evaluate(context);
        int stack = Mathf.FloorToInt(effectData.InitStack.Evaluate(context));

        if (effectData.ExclusiveGroup == null)
        {
            oldEffect.Refresh(context, oldEffect.Stack + stack, Mathf.Max(duration, oldEffect.RemainingTime));
            return;
        }

        IStatusEffect baseEffect = effectData.ExclusiveGroup.StatusEffectSelector.Select(oldEffect, newEffect);
        
        bool isBaseOld = baseEffect == oldEffect;

        duration = effectData.ExclusiveGroup.DurationSelector.Select(baseEffect.RemainingTime, oldEffect.RemainingTime,
            newEffect.RemainingTime);
        stack = effectData.ExclusiveGroup.StackSelector.Select(baseEffect.Stack, oldEffect.Stack, newEffect.Stack);
        
        baseEffect.Refresh(context, stack, duration);
        _effects[id] = baseEffect;

        if (isBaseOld)
        {
            newEffect.Remove();
        }
        else
        {
            oldEffect.Remove();
        }
    }

    public void DeleteByID(string id)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteByTag(string tag)
    {
        throw new System.NotImplementedException();
    }
}