using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.Conditions;

public sealed class CountData : ConditionData
{
    public readonly IFormula<SkillContext> count;

    public CountData(IFormulaDefinition<SkillContext> count)
    {  
        this.count = count.Compile();
    }

    public override Condition Create()
    {
        return Count.Get(this);
    }
}