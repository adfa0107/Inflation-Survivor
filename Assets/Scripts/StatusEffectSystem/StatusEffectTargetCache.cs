using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffect;

public static class StatusEffectTargetCache
{
    private static readonly Dictionary<int, IStatusEffectTarget> _cache = new Dictionary<int, IStatusEffectTarget>();

    public static bool TryGetStatusEffectTarget(GameObject gameObject, out IStatusEffectTarget statusEffectTarget)
    {
        Assert.IsNotNull(gameObject, "[StatusEffectTargetCache] [TryGetStatusEffectTarget] gameObject is null.");

        if (gameObject == null)
        {
            statusEffectTarget = null;
            return false;
        }
        
        return _cache.TryGetValue(gameObject.GetInstanceID(), out statusEffectTarget);
    }

    public static bool TryGetStatusEffectTarget<TComponent>(TComponent component, out IStatusEffectTarget statusEffectTarget)
        where TComponent : Component
    {
        Assert.IsNotNull(component, "[StatusEffectTargetCache] [TryGetStatusEffectTarget] component is null.");

        if (component == null)
        {
            statusEffectTarget = null;
            return false;
        }
        
        return _cache.TryGetValue(component.gameObject.GetInstanceID(), out statusEffectTarget);
    }

    public static void AddStatusEffectTarget(GameObject gameObject, IStatusEffectTarget statusEffectTarget)
    {
        Assert.IsNotNull(gameObject, "[StatusEffectTargetCache] [AddStatusEffectTarget] gameObject is null");
        Assert.IsNotNull(statusEffectTarget, "[StatusEffectTargetCache] [AddStatusEffectTarget] statusEffectTarget is null");
        Assert.IsFalse(_cache.ContainsKey(gameObject.GetInstanceID()), "[StatusEffectTargetCache] [AddStatusEffectTarget] gameObject is already exists");

        if (gameObject == null || statusEffectTarget == null)
        {
            return;
        }
        
        _cache[gameObject.GetInstanceID()] = statusEffectTarget;
    }

    public static void RemoveStatusEffectTarget(GameObject gameObject)
    {
        Assert.IsNotNull(gameObject, "[StatusEffectTargetCache] [RemoveStatusEffectTarget] gameObject is null");
        
        if (gameObject == null)
        {
            return;
        }
        
        _cache.Remove(gameObject.GetInstanceID());
    }
}