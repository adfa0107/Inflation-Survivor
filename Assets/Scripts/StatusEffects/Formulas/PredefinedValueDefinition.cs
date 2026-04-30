using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.StatusEffects.Formulas;

[Serializable]
public class PredefinedValueDefinition : IFormulaDefinition<StatusEffectContext>
{
    [SerializeField] private PredefinedValueName valueName;
    
    public IFormula<StatusEffectContext> Build()
    {
        return new PredefinedValue(valueName);
    }
}