using System;

namespace InflationSurvivor.Skills.Primitives;

[Serializable]
public abstract class FormulaDefinition
{
    public abstract Formula Compile();
}