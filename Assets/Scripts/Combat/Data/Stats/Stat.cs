using System;
using System.Collections.Generic;
using InflationSurvivor.Combat.Handles;
using UnityEngine.Assertions;

namespace InflationSurvivor.Combat.Data.Stats;

public readonly struct Stat
{
    private static uint _idCounter = 0;
    private static readonly int _statCount = Enum.GetValues(typeof(StatType)).Length;
    private static readonly int _componentCount = Enum.GetValues(typeof(StatModifierType)).Length;

    private readonly float[][] _statComponents;
    private readonly float[] _stat;
    private readonly Dictionary<StatModifyHandle, StatModifier> _modifiers;

    public Stat()
    {
        _statComponents = new float[_statCount][];

        for (int i = 0; i < _statCount; i++)
        {
            _statComponents[i] = new float[_componentCount];
        }
        
        _stat = new float[_statCount];
        _modifiers = new Dictionary<StatModifyHandle, StatModifier>();
    }
    
    public float this[StatType type] => _stat[(int)type];

    public void Reset(IReadOnlyDictionary<StatType, float> stats)
    {
        _modifiers.Clear();
        
        for (int i = 0; i < _statCount; i++)
        {
            for (int j = 0; j < _componentCount; j++)
            {
                _statComponents[i][j] = 0;
            }
        }

        foreach ((StatType type, float value) in stats)
        {
            _statComponents[(int)type][(int)StatModifierType.Base] = value;
            _stat[(int)type] = value;
        }
    }

    public StatModifyHandle AddModifier(StatModifier modifier)
    {
        switch (modifier.statModifierType)
        {
            case StatModifierType.Base:
            case StatModifierType.Flat:
            case StatModifierType.AdditivePercent:
                _statComponents[(int)modifier.statType][(int)modifier.statModifierType] += modifier.value;
                break;
            case StatModifierType.MultiplicativePercent:
                _statComponents[(int)modifier.statType][(int)modifier.statModifierType] *= modifier.value;
                break;
        }
        
        var modifyHandle = new StatModifyHandle(_idCounter++);
        _modifiers.Add(modifyHandle, modifier);
        
        return modifyHandle;
    }

    public void RemoveModifier(StatModifyHandle handle)
    {
        Assert.IsTrue(_modifiers.ContainsKey(handle));
        
        StatModifier modifier = _modifiers[handle];
        
        switch (modifier.statModifierType)
        {
            case StatModifierType.Base:
            case StatModifierType.Flat:
            case StatModifierType.AdditivePercent:
                _statComponents[(int)modifier.statType][(int)modifier.statModifierType] -= modifier.value;
                break;
            case StatModifierType.MultiplicativePercent:
                _statComponents[(int)modifier.statType][(int)modifier.statModifierType] /= modifier.value;
                break;
        }
        
        _modifiers.Remove(handle);
    }
}