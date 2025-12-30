using InflationSurvivor.CombatData;
using InflationSurvivor.SkillSystem.Core;

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

    public override bool IsActive(SkillCastModule caster)
    {
        _currentCount -= 1;
        return _currentCount <= 0;
    }

    public override void Deactivate(SkillCastModule caster)
    {
        _currentCount = _count.GetScaledValueAsInt(caster.StatModule);
    }
}