using System.Collections.Generic;
using System.Collections.Immutable;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Data;

public record struct ActionData(
    float delay, 
    CastData cast, 
    IReadOnlyList<SkillEffect> effects
    )
{
    public ActionData(ActionDefinition actionDefinition) : 
        this(
            actionDefinition.Delay, 
            actionDefinition.Cast.GetData(), 
            actionDefinition.Effects
            ) 
    { }
};