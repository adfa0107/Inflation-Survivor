using System;
using System.Collections.Generic;

namespace InflationSurvivor.CombatData.ResourceSystem;

public readonly struct Resource
{
    private static readonly int _costCount = Enum.GetValues(typeof(ResourceType)).Length;

    private readonly ResourceValue[] _resources;
    public ref ResourceValue this[ResourceType resourceType] => ref _resources[(int)resourceType];

    public Resource()
    {
        _resources = new ResourceValue[_costCount];
    }

    public void Reset(IReadOnlyDictionary<ResourceType, ResourceValue> resources)
    {
        foreach ((ResourceType type, ResourceValue resource) in resources)
        {
            _resources[(int)type] = resource;
        }
        
        foreach (ResourceValue resource in _resources)
        {
            resource.Reset();
        }
    }

    public void Update(float deltaTime)
    {
        foreach (ResourceValue resource in _resources)
        {
            resource.Restore(resource.Regeneration * deltaTime);
        }
    }
}