using System;
using System.Collections.Generic;

namespace InflationSurvivor.Combat.Data.CombatResources;

public readonly struct CombatResource
{
    private static readonly int _costCount = Enum.GetValues(typeof(CombatResourceType)).Length;

    private readonly CombatResourceValue[] _resources;
    public ref CombatResourceValue this[CombatResourceType combatResourceType] => ref _resources[(int)combatResourceType];

    public CombatResource()
    {
        _resources = new CombatResourceValue[_costCount];
    }

    public void Reset(IReadOnlyDictionary<CombatResourceType, CombatResourceValue> resources)
    {
        foreach ((CombatResourceType type, CombatResourceValue resource) in resources)
        {
            _resources[(int)type] = resource;
        }
        
        foreach (CombatResourceValue resource in _resources)
        {
            resource.Reset();
        }
    }

    public void Update(float deltaTime)
    {
        foreach (CombatResourceValue resource in _resources)
        {
            resource.Restore(resource.Regeneration * deltaTime);
        }
    }
}