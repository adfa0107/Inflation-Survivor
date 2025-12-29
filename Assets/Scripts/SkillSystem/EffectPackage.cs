using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;

namespace InflationSurvivor.SkillSystem;

public sealed class EffectPackage
{
    private static readonly SimplePool<EffectPackage> _pool = new();

    private SkillContext _context;
    private IReadOnlyList<Effect> _effects;
    private readonly ISkillTarget[] _singleTarget = new ISkillTarget[1]; 

    public static EffectPackage Get(SkillContext context, IReadOnlyList<Effect> effects)
    {
        EffectPackage package = _pool.Get();
        package.Initialize(context, effects);
        return package;
    }

    private void Initialize(SkillContext context, IReadOnlyList<Effect> effects)
    {
        _context = context;
        _effects = effects;
    }

    public void Release()
    {
        _effects = null;
        _context = null;
        _singleTarget[0] = null;
        _pool.Release(this);
    }

    public void Apply(IReadOnlyList<ISkillTarget> targets)
    {
        foreach (Effect effect in _effects)
        {
            effect.ApplyEffect(_context, targets);
        }
    }

    public void Apply(ISkillTarget target)
    {
        _singleTarget[0] = target;
        Apply(_singleTarget);
    }
}