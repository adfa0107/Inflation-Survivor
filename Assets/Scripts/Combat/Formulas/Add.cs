using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Core;

namespace InflationSurvivor.Combat.Formulas;

public class Add<TContext> : IFormula<TContext> where TContext : struct
{
    private readonly IFormula<TContext>[] _formulas;

    public Add(IFormulaDefinition<TContext>[] formulas)
    {
        _formulas = new IFormula<TContext>[formulas.Length];

        for (int i = 0; i < _formulas.Length; i++)
        {
            _formulas[i] = formulas[i].Compile();
        }
    }
    
    public float Evaluate(TContext context)
    {
        float result = 0;
        foreach (IFormula<TContext> formula in _formulas)
        {
            result += formula.Evaluate(context);
        }
        
        return result;
    }
}