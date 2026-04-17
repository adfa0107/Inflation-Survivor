using System;
using InflationSurvivor.Combat.Abstractions;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.CombatResources;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core;
using InflationSurvivor.Core.Attributes;
using InflationSurvivor.Skills.Primitives;
using InflationSurvivor.Skills.Primitives.Positions;
using InflationSurvivor.Skills.Primitives.Targets;
using UnityEngine;

namespace InflationSurvivor.Skills
{
    [CreateAssetMenu(menuName = "Inflation Survivor/Skill"), Serializable]
    public sealed class SkillDefinition : SkillDefinitionBase
    {
        [SerializeField] private string id;
        
        [SerializeField] private string skillName;
        [SerializeField] private Sprite icon;

        [SerializeField] private CombatResourceType costType;
        [SerializeField, SerializeReference, FormulaSelector] 
        private IFormulaDefinition<SkillContext> cost;
        [SerializeField, SerializeReference, FormulaSelector] 
        private IFormulaDefinition<SkillContext> cooldown;

        [SerializeField, SerializeReference, SubclassSelector]
        private ConditionDefinition[] conditions;

        [SerializeField] private TargetActionDefinition[] targetActions;
        [SerializeField] private PositionActionDefinition[] positionActions;

        public override ISkillData CreateData() => new SkillData(id, skillName, icon, costType, cost, cooldown, conditions, targetActions, positionActions);
    }
}

