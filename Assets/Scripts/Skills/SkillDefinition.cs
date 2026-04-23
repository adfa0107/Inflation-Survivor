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

        public override ISkillData Build()
        {
            var builtConditions = new ConditionData[conditions.Length];
            var builtTargetActions = new TargetAction[targetActions.Length];
            var builtPositionActions = new PositionAction[positionActions.Length];

            for (var i = 0; i < conditions.Length; i++)
            {
                builtConditions[i] = conditions[i].Build();
            }

            for (var i = 0; i < targetActions.Length; i++)
            {
                builtTargetActions[i] = targetActions[i].Build();
            }

            for (var i = 0; i < positionActions.Length; i++)
            {
                builtPositionActions[i] = positionActions[i].Build();
            }
            
            return new SkillData(id, skillName, icon, costType, cost.Build(), cooldown.Build(), builtConditions, builtTargetActions, builtPositionActions);
        }
    }
}

