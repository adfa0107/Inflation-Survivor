using System;
using InflationSurvivor.Combat.Abstractions;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Formulas;

[Serializable]
public sealed class ConstantDefinition : NoContextRequireFormulaDefinition
{
    [SerializeField] private float value;
    protected override NoContextRequireFormula Build()
    {
        return new Constant(value);
    }
}