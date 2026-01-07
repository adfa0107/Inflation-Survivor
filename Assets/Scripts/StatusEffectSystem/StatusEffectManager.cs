using System.Collections.Generic;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffect;

public class StatusEffectManager
{
    public readonly Stat stat;
    public readonly Resource resource;
    
    private readonly Dictionary<string, StatusEffectInstance> _statusEffects;
    private readonly List<KeyValuePair<string, StatusEffectInstance>> _needToRemove;

    public StatusEffectManager(Stat stat, Resource resource)
    {
        this.stat = stat;
        this.resource = resource;
        _statusEffects = new Dictionary<string, StatusEffectInstance>();
        _needToRemove = new List<KeyValuePair<string, StatusEffectInstance>>(8);
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
        foreach (KeyValuePair<string, StatusEffectInstance> effectPair in _statusEffects)
        {
            effectPair.Value.Update(deltaTime);
            if (effectPair.Value.RemainingTime <= 0f)
            {
                _needToRemove.Add(effectPair);
            }
        }

        foreach (KeyValuePair<string, StatusEffectInstance> effectPair in _needToRemove)
        {
            _statusEffects.Remove(effectPair.Key);
            effectPair.Value.Remove();
        }
        
        _needToRemove.Clear();
    }
}