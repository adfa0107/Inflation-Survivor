using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using InflationSurvivor.SkillSystem.Interfaces;

namespace InflationSurvivor.SkillSystem.Core;

public class SkillContext
{
    private static readonly SimplePool<SkillContext> _pool = new SimplePool<SkillContext>();

    public ISkillCaster caster;
    public ISkillTarget target;
    
    public readonly Dictionary<string, int> @int = new Dictionary<string, int>();
    public readonly Dictionary<string, float> @float = new Dictionary<string, float>();
    public readonly Dictionary<string, bool> @bool = new Dictionary<string, bool>();
    
    public static SkillContext Get() => _pool.Get();

    public void Release()
    {
        @int.Clear();
        @float.Clear();
        @bool.Clear();
        _pool.Release(this);
    }
}