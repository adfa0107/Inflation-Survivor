using System;
using InflationSurvivor.CombatSystem;

namespace InflationSurvivor.Skills.Primitives.Targets;

[Serializable]
public abstract class TargetSourceDefinition
{
    public abstract TargetSource Compile(ISkillProcessor<CombatModule> processor);
}