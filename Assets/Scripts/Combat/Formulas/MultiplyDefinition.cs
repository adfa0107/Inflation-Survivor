using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Formulas;

[Serializable]
public class MultiplyDefinition<TContext> : IFormulaDefinition<TContext> where TContext : struct
{
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormulaDefinition<TContext>[] formulas;

    public IFormula<TContext> Build() => new Multiply<TContext>(formulas);
}