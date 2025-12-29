using adfa.Utility.ObjectPool;
using InflationSurvivor.SkillSystem.Interfaces;

namespace InflationSurvivor.SkillSystem.Core;

public abstract class ConditionInstance
{
    public abstract void Reset();
    public void Release()
    {
        ReleaseInternal();
    }

    protected abstract void ReleaseInternal();
    
    public abstract bool IsActive(ISkillCaster caster);
    public abstract void Deactivate(ISkillCaster caster);
}

public abstract class ConditionInstance<TSelf, TData> : ConditionInstance, IInstance<TData>
    where TSelf : ConditionInstance<TSelf, TData>, new()
    where TData : ConditionData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>();
    
    public static TSelf Get(TData data) => _pool.Get(data);
    protected override void ReleaseInternal() => _pool.Release((TSelf)this);
    
    public abstract void Create(TData data);
}