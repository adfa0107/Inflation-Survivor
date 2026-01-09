using System;
using System.Collections.Generic;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.CombatData.StatSystem;
using InflationSurvivor.SkillSystem.Core;
using SerializeReferenceEditor;
using UnityEngine;

namespace InflationSurvivor.SkillSystem
{
    [CreateAssetMenu(menuName = "Inflation Survivor/Skill"), Serializable]
    public sealed class SkillDefinition : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: SerializeField] public ResourceType CostType { get; private set; }
        [field: SerializeField] public ScaledValue Cost { get; private set; }
        [field: SerializeField] public ScaledValue Cooldown { get; private set; }
        
        [field: SerializeField, SerializeReference, SR(typeof(ConditionDefinition))] public List<ConditionDefinition> Conditions { get; private set; }
        [field: SerializeField] public List<ActionDefinition> Actions { get; private set; }
    }
}

