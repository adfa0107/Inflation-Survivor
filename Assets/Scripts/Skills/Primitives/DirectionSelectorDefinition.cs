using System;

namespace InflationSurvivor.Skills.Primitives;

[Serializable]
public abstract class DirectionSelectorDefinition
{
    public abstract DirectionSelector Build();
}