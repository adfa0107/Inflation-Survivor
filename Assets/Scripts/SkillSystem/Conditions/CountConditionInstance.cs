using InflationSurvivor.CombatSystem.StatSystem;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem.Conditions;

public class CountConditionInstance : ConditionInstance<CountConditionInstance, CountConditionData>
{     
    private ScaledValue _count;
    private int _currentCount;
        
    public override void Setup(CountConditionData data)
    {
        _count = data.Count;
    }

    public override void Reset() { }

    public override bool IsActive(SkillCastModule caster)
    {
        _currentCount -= 1;
        return _currentCount <= 0;
    }

    public override void Deactivate(SkillCastModule caster)
    {
        _currentCount = _count.GetScaledValueAsInt(caster.stat);
    }
}