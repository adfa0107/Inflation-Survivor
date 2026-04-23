using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.Conditions;

public sealed class CountData : ConditionData
{
    public readonly IFormula<SkillContext> count;

    public CountData(IFormula<SkillContext> count)
    {  
        this.count = count;
    }

    public override Condition Create()
    {
        return Count.Get(this);
    }
}