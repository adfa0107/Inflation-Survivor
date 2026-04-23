using System;
using InflationSurvivor.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public class ExclusiveGroupDefinition : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private BaseSelectPolicy @base;
    [SerializeField] private MergePolicy duration;
    [SerializeField] private MergePolicy stack;

    public ExclusiveGroup Build()
    {
        Assert.IsFalse(string.IsNullOrEmpty(id));
        if (DataBase<ExclusiveGroup>.TryGet(id, out ExclusiveGroup exclusiveGroup))
        {
            Assert.IsTrue(exclusiveGroup.@base == @base);
            Assert.IsTrue(exclusiveGroup.stack == stack);
            Assert.IsTrue(exclusiveGroup.duration == duration);
            
            return exclusiveGroup;
        }
        
        exclusiveGroup = new ExclusiveGroup(id, @base, duration, stack);
        DataBase<ExclusiveGroup>.Register(exclusiveGroup);
        
        return exclusiveGroup;
    }
}