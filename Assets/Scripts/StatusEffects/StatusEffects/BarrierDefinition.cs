using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.StatusEffects
{
    [CreateAssetMenu(menuName = "Inflation Survivor/StatusEffects/Barrier")]
    public class BarrierDefinition : StatusEffectDefinition
    {
        [SerializeField, SerializeReference, FormulaSelector]
        private IFormulaDefinition<StatusEffectContext> amount;
    
        public override IStatusEffectData Build()
        {
            return new BarrierData(id, effectName, icon, priority, exclusiveGroup.Build(), duration.Build(), initStack.Build(), maxStack.Build(), amount.Build());
        }
    }
}