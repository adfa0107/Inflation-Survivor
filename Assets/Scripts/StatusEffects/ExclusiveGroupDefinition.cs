using System;
using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.StatusEffects
{
    [Serializable, CreateAssetMenu(menuName = "Inflation Survivor/ExclusiveGroup")]
    public class ExclusiveGroupDefinition : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private EffectSelectMethod effectSelectMethod;
        [SerializeField] private EffectValueSelectMethod durationSelectMethod;
        [SerializeField] private EffectValueSelectMethod stackSelectMethod;

        public IExclusiveGroup Build()
        {
            Assert.IsFalse(string.IsNullOrEmpty(id));
            if (DataBase<IExclusiveGroup>.TryGet(id, out IExclusiveGroup exclusiveGroup))
            {
                return exclusiveGroup;
            }
        
            exclusiveGroup = new ExclusiveGroup(id, new StatusEffectSelector(effectSelectMethod), new StatusEffectValueSelector(durationSelectMethod), new StatusEffectValueSelector(stackSelectMethod));
            DataBase<IExclusiveGroup>.Register(exclusiveGroup);
        
            return exclusiveGroup;
        }
    }
}