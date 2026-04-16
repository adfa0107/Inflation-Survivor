using InflationSurvivor.Skills.Primitives;

namespace InflationSurvivor.Skills.Conditions;

public sealed class CountData : ConditionData
{
    public readonly Formula count;

    public CountData(FormulaDefinition count)
    {  
        this.count = count.Compile();
    }

    public override Condition Create()
    {
        return Count.Get(this);
    }
}