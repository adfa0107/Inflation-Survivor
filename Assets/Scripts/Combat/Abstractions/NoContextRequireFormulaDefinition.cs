using System;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Abstractions;

[Serializable]
public abstract class NoContextRequireFormulaDefinition : IFormulaDefinition<SkillContext>, IFormulaDefinition<StatusEffectContext>
{
    protected abstract NoContextRequireFormula Build();
    
    IFormula<SkillContext> IFormulaDefinition<SkillContext>.Build() => Build();

    IFormula<StatusEffectContext> IFormulaDefinition<StatusEffectContext>.Build() => Build();
}