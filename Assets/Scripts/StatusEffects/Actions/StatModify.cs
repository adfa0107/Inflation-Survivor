using System.Collections.Generic;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Handles;

namespace InflationSurvivor.StatusEffects.Actions;

public class StatModify : StatusEffectAction<StatModify, StatModifyData>
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
            _handles.Add(owner.stat.AddModifier(modifier));
        }
    }

    protected override void OnRemove()
    {
        foreach (StatModifyHandle handle in _handles)
        {
            owner.stat.RemoveModifier(handle);
        }
        _handles.Clear();
    }

    public override void Update(float tick)
    {
        
    }
}