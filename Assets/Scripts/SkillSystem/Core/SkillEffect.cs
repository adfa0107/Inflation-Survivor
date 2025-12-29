using System;
using System.Collections.Generic;
using InflationSurvivor.SkillSystem.Interfaces;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public abstract class SkillEffect
{
    public abstract void ApplyEffect(SkillContext context, IReadOnlyList<ISkillTarget> targets);
}