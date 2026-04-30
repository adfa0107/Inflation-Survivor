using System;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public abstract class StatusEffectActionDefinition
{
    public abstract StatusEffectActionData Build();
}