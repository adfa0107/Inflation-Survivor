using InflationSurvivor.CombatData.StatSystem;
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

    public override void Dispose() { }

    public override bool CanActivate(SkillCastModule caster)
    {
        return _currentCount <= 1;
    }

    public override void Deactivate(SkillCastModule caster)
    {
        _currentCount = _count.GetScaledValueAsInt(caster.stat);
    }
    
    public override void Update(SkillCastModule caster)
    {
        _currentCount -= 1;
    }
    
    protected override bool IsActive()
    {
        return _currentCount <= 0;
    }
}