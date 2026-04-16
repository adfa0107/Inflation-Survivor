using System;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Formulas;

[Serializable]
public sealed class ConstantDefinition : FormulaDefinition
{
    [SerializeField] private float value;
    
    public override Formula Compile()
    {
        return new Constant(value);
    }
}