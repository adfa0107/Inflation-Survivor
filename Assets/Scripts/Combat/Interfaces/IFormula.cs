namespace InflationSurvivor.Combat.Interfaces;

public interface IFormula<in TContext> where TContext : struct
{
    public float Evaluate(TContext context);
}