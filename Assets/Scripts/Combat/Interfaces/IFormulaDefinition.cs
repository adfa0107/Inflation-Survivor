namespace InflationSurvivor.Combat.Interfaces;

public interface IFormulaDefinition<in TContext> where TContext : struct
{
    public IFormula<TContext> Compile();
}