using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Formulas;

public class Multiply<TContext>: IFormula<TContext> where TContext : struct
{
    private readonly IFormula<TContext>[] _formulas;

    public Multiply(IFormulaDefinition<TContext>[] formulas)
    {
        _formulas = new IFormula<TContext>[formulas.Length];

        for (int i = 0; i < _formulas.Length; i++)
        {
            _formulas[i] = formulas[i].Build();
        }
    }
    
    public float Evaluate(TContext context)
    {
        float result = 1;
        foreach (IFormula<TContext> formula in _formulas)
        {
            result *= formula.Evaluate(context);
        }
        
        return result;
    }
}