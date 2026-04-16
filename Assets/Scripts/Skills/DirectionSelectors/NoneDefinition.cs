using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.DirectionSelectors;

public class NoneDefinition : DirectionSelectorDefinition
{
    public override DirectionSelector Compile()
    {
        return new None();
    }
}