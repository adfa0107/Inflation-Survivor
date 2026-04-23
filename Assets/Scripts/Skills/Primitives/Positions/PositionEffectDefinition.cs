using System;

namespace InflationSurvivor.Skills.Primitives.Positions;

[Serializable]
public abstract class PositionEffectDefinition
{
    public abstract PositionEffect Build();
}