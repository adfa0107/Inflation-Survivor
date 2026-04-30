using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Data.Stats;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.StatusEffects
{
    [CreateAssetMenu(menuName = "Inflation Survivor/StatusEffects/Stat Modify")]
    public class StatModifyDefinition : StatusEffectDefinition
    {
        [SerializeField] private StatFormulaModifierDefinition<StatusEffectContext>[] modifiers;
        
        public override IStatusEffectData Build()
        {
            StatFormulaModifier<StatusEffectContext>[] builtModifiers = new StatFormulaModifier<StatusEffectContext>[modifiers.Length];
            for (int i = 0; i < modifiers.Length; i++)
            {
                builtModifiers[i] = modifiers[i].Build();
            }
            
            return new StatModifyData(id, name, icon, priority, exclusiveGroup.Build(), duration.Build(), initStack.Build(), maxStack.Build(), builtModifiers);
        }
    }
}