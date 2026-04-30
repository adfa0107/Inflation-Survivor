using System.Collections.Generic;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Handles;

namespace InflationSurvivor.StatusEffects.StatusEffects;

public class StatModify : StatusEffect<StatModify, StatModifyData>
{
    private readonly List<StatModifyHandle> _handles = new List<StatModifyHandle>();
    
    protected override void OnSetup()
    {
        if (_handles.Capacity < data.modifier.Length)
        {
            _handles.Capacity = data.modifier.Length;
        }
    }

    protected override void OnDispose()
    {
        _handles.Clear();
    }

    protected override void OnApply(StatusEffectContext context)
    {
        for (int i = 0; i < data.modifier.Length; i++)
        {
            StatModifier modifier = data.modifier[i].MakeModifier(context);
            _handles.Add(context.target.stat.AddModifier(modifier));
        }
    }

    protected override void OnRemove(CombatModule owner)
    {
        foreach (StatModifyHandle handle in _handles)
        {
            owner.stat.RemoveModifier(handle);
        }
        _handles.Clear();
    }

    protected override void OnUpdate(CombatModule owner, float tick)
    {
        
    }
}