using System;
using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.DirectionSelectors;

[Serializable]
public class NoneDefinition : DirectionSelectorDefinition
{
    public override DirectionSelector Compile()
    {
        return new None();
    }
}