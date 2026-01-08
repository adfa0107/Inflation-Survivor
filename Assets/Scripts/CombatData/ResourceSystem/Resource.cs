using System;
using System.Collections.Generic;

namespace InflationSurvivor.CombatData.ResourceSystem;

public class Resource
{
    private static readonly int _costCount = Enum.GetValues(typeof(ResourceType)).Length;
    
    private readonly ResourceValue[] _resources = new ResourceValue[_costCount];
    public ResourceValue this[ResourceType resourceType] => _resources[(int)resourceType];

    public Resource()
    {
        for (int i = 0; i < _costCount; i++)
        {
            _resources[i] = new ResourceValue();
        }
    }

    public void Reset(IReadOnlyDictionary<ResourceType, ResourceStat> resourceStats)
    {
        foreach (ResourceValue resourceValue in _resources)
        {
            resourceValue.Reset(default);
        }

        foreach ((ResourceType type, ResourceStat stat) in resourceStats)
        {
            _resources[(int)type].Reset(stat);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (ResourceValue resource in _resources)
        {
            resource.Restore(resource.stat.Regeneration * deltaTime);
        }
    }
}