using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatSystem;

namespace InflationSurvivor.SkillSystem.Conditions;

public class CountConditionInstance : ConditionInstance<CountConditionInstance, CountConditionData>
{     
    private ScaledValue _count;
    private int _currentCount;
        
    public override void Create(CountConditionData data)
    {
        _count = data.Count;
        Reset();
    }

    public override void Reset()
    {
        _currentCount = 0;
    }

    public override bool IsActive(ISkillCaster caster)
    {
        _currentCount -= 1;
        return _currentCount <= 0;
    }

    public override void Deactivate(ISkillCaster caster)
    {
        _currentCount = _count.GetScaledValueAsInt(caster);
    }
}