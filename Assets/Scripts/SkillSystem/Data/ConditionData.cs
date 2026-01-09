using InflationSurvivor.Core;
using InflationSurvivor.SkillSystem.Core;
using Newtonsoft.Json;

namespace InflationSurvivor.SkillSystem.Data;

[JsonConverter(typeof(PolyConverter<ConditionData>))]
public abstract record ConditionData()
{
    public abstract ConditionInstance CreateInstance();
}