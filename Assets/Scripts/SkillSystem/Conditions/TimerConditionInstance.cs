using System.Collections;
using adfa.Utility;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Conditions;

public class TimerConditionInstance : ConditionInstance<TimerConditionInstance, TimerConditionData>
{
    private ScaledValue _cooldown;
    private float _lastTime;

    public override void Create(TimerConditionData conditionData)
    {
        _cooldown = conditionData.Cooldown;
    }

    public override void Reset()
    {
        _lastTime = float.NegativeInfinity;
    }

    public override bool IsActive(ISkillCaster caster)
    {
        return Time.time - _lastTime >= _cooldown.GetScaledValue(caster);
    }

    public override void Deactivate(ISkillCaster caster)
    {
        _lastTime = Time.time;
    }
}