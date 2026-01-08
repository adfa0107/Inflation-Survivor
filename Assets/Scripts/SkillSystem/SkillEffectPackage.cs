using System;
using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.SkillSystem;

public sealed class SkillEffectPackage : IDisposable
{
    private static readonly SimplePool<SkillEffectPackage> _pool = new(100);

    private SkillCastModule _caster;
    private GameEvent _event;
    private IReadOnlyList<SkillEffect> _effects;
    private readonly CombatModule[] _singleTarget = new CombatModule[1];

    public static SkillEffectPackage Get(SkillCastModule caster, GameEvent @event, IReadOnlyList<SkillEffect> effects)
    {
        SkillEffectPackage package = _pool.Get();
        package._caster = caster;
        package._event = @event;
        package._effects = effects;
        return package;
    }

    public void Release() => _pool.Release(this);

    public void Dispose()
    {
        _caster = null;
        _event = null;
        _effects = null;
        _singleTarget[0] = null;
    }

    public void Apply(IReadOnlyList<CombatModule> targets)
    {
        foreach (SkillEffect effect in _effects)
        {
            effect.ApplyEffect(_caster, _event, targets);
        }
    }

    public void Apply(CombatModule target)
    {
        _singleTarget[0] = target;
        Apply(_singleTarget);
    }
}