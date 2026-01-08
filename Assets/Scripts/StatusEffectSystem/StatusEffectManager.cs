using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using UnityEngine.Assertions;
using EventHandler = InflationSurvivor.EventSystem.EventHandler;

namespace InflationSurvivor.StatusEffect;

public class StatusEffectManager
{
    public readonly Stat stat;
    public readonly Resource resource;
    public readonly EventHandler eventHandler;
    
    private readonly Dictionary<string, StatusEffectInstance> _statusEffects;
    private readonly List<ValueTuple<string, StatusEffectInstance>> _needToRemove;

    public StatusEffectManager(Stat stat, Resource resource, EventHandler eventHandler)
    {
        this.stat = stat;
        this.resource = resource;
        this.eventHandler = eventHandler;
        _statusEffects = new Dictionary<string, StatusEffectInstance>();
        _needToRemove = new List<ValueTuple<string, StatusEffectInstance>>(8);
    }

    public bool TryGetStatusEffect(string id, out StatusEffectInstance statusEffect)
    {
        return _statusEffects.TryGetValue(id, out statusEffect);
    }

    public void AddStatusEffect(string id, StatusEffectInstance statusEffect)
    {
        Assert.IsFalse(_statusEffects.ContainsKey(id));
        _statusEffects[id] = statusEffect;
        statusEffect.Apply(this);
    }

    public void RemoveStatusEffect(string id)
    {
        Assert.IsTrue(_statusEffects.ContainsKey(id));
        StatusEffectInstance statusEffect = _statusEffects[id];
        _statusEffects.Remove(id);
        statusEffect.Remove();
    }

    public void ChangeStatusEffect(string id, StatusEffectInstance newStatusEffect)
    {
        Assert.IsTrue(_statusEffects.ContainsKey(id));
        StatusEffectInstance oldStatusEffect = _statusEffects[id];
        oldStatusEffect.Remove();
        _statusEffects[id] = newStatusEffect;
        newStatusEffect.Apply(this);
    }

    public void Update(float deltaTime)
    {
        foreach ((string id, StatusEffectInstance effect) in _statusEffects)
        {
            effect.Update(deltaTime);
            if (effect.RemainingTime <= 0f)
            {
                _needToRemove.Add((id, effect));
            }
        }

        foreach ((string id, StatusEffectInstance effect) in _needToRemove)
        {
            _statusEffects.Remove(id);
            effect.Remove();
        }
        
        _needToRemove.Clear();
    }
}