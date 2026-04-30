using System;
using InflationSurvivor.Combat.Abstractions;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using InflationSurvivor.Core.Attributes;
using UnityEngine;

namespace InflationSurvivor.StatusEffects
{
    [CreateAssetMenu(menuName = "Inflation Survivor/StatusEffect")]
    public sealed class StatusEffectDefinition : StatusEffectDefinitionBase
    {
        [SerializeField] private StatusEffectInfoDefinition info;
        [SerializeField, SerializeReference, SubclassSelector]
        private StatusEffectActionDefinition[] actions;

        public override IStatusEffectData Build()
        {
            StatusEffectActionData[] builtActions = new StatusEffectActionData[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                builtActions[i] = actions[i].Build();
            }
        
            return new StatusEffectData(info.Build(), builtActions);
        }
    }
}