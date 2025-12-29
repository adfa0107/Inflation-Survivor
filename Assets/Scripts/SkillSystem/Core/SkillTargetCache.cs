using System.Collections.Generic;
using InflationSurvivor.SkillSystem.Interfaces;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Core;

public class SkillTargetCache
{
    private static readonly Dictionary<int, ISkillTarget> _cache = new Dictionary<int, ISkillTarget>();

    public static void Register(Collider2D collider, ISkillTarget skillTarget)
    {
        _cache[collider.GetInstanceID()] = skillTarget;
    }

    public static void Unregister(Collider2D collider)
    {
        _cache.Remove(collider.GetInstanceID());
    }

    public static bool TryGetSkillTarget(Collider2D collider, out ISkillTarget skillTarget)
    {
        return _cache.TryGetValue(collider.GetInstanceID(), out skillTarget);
    }
}