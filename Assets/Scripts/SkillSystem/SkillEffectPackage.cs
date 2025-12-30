using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using InflationSurvivor.CombatData;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem;

public sealed class SkillEffectPackage
{
    private static readonly SimplePool<SkillEffectPackage> _pool = new(100);

    private SkillCastModule _caster;
    private GameEventData _eventData;
    private IReadOnlyList<SkillEffect> _effects;
    private readonly CombatModule[] _singleTarget = new CombatModule[1];

    public static SkillEffectPackage Get(SkillCastModule caster, GameEventData eventData, IReadOnlyList<SkillEffect> effects)
    {
        SkillEffectPackage package = _pool.Get();
        package._caster = caster;
        package._eventData = eventData;
        package._effects = effects;
        return package;
    }

    public void Release()
    {
        _caster = null;
        _eventData = null;
        _effects = null;
        _singleTarget[0] = null;
        _pool.Release(this);
    }

    public void Apply(IReadOnlyList<CombatModule> targets)
    {
        foreach (SkillEffect effect in _effects)
        {
            effect.ApplyEffect(_caster, _eventData, targets);
        }
    }

    public void Apply(CombatModule target)
    {
        _singleTarget[0] = target;
        Apply(_singleTarget);
    }
}