using System;
using InflationSurvivor.CombatData.ResourceSystem;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills
{
    [CreateAssetMenu(menuName = "Inflation Survivor/Skill"), Serializable]
    public sealed class SkillDefinition : ScriptableObject
    {
        [SerializeField] private string skillName;
        [SerializeField] private Sprite icon;

        [SerializeField] private ResourceType costType;
        [SerializeField, SerializeReference, SubclassSelector] 
        private FormulaDefinition cost;
        [SerializeField, SerializeReference, SubclassSelector] 
        private FormulaDefinition cooldown;

        [SerializeField, SerializeReference, SubclassSelector]
        private ConditionDefinition[] conditions;

        [SerializeField] private TargetActionDefinition[] targetActions;
        [SerializeField] private PositionActionDefinition[] positionActions;

        public SkillData CreateData() => new SkillData(skillName, icon, costType, cost, cooldown, conditions, targetActions, positionActions);
    }
}

