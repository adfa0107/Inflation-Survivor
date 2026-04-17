using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Skills.Primitives;
using UnityEngine;

namespace InflationSurvivor.Skills.Conditions;

public sealed class Count : Condition<Count, CountData>
{
    private int _currentCount;

    protected override void Setup()
    {
        _currentCount = 0;
    }

    public override void Dispose() { }

    public override bool CanActivate(SkillContext context)
    {
        return _currentCount <= 1;
    }

    public override void Deactivate(SkillContext context)
    {
        _currentCount = Mathf.FloorToInt(data.count.Evaluate(context));
    }
    
    public override void Update(SkillContext context)
    {
        _currentCount -= 1;
    }
    
    protected override bool IsActive()
    {
        return _currentCount <= 0;
    }
}