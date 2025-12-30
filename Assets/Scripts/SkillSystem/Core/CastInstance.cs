using adfa.Utility.ObjectPool;

namespace InflationSurvivor.SkillSystem.Core;

public abstract class CastInstance
{
    public abstract void Cast(SkillCastModule caster, SkillEffectPackage effectPackage);
    public abstract void Release();
}

public abstract class CastInstance<TSelf, TData> : CastInstance, IInstance<TData>
    where TSelf : CastInstance<TSelf, TData>, new()
    where TData : CastData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>();
        
    public static TSelf Get(TData data) => _pool.Get(data);
    public abstract void Create(TData data);
    public override void Release() => _pool.Release((TSelf)this);
}