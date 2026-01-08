using InflationSurvivor.Core.ObjectPool;

namespace InflationSurvivor.SkillSystem.Core;

public abstract class ConditionInstance
{
    public abstract void Release();

    public abstract bool CanActivate(SkillCastModule caster);
    public abstract void Deactivate(SkillCastModule caster);
    public abstract void Update(SkillCastModule caster);
    
    
    protected abstract bool IsActive();
    
    public static implicit operator bool(ConditionInstance instance)
    {
        return instance is not null && instance.IsActive();
    }
}

public abstract class ConditionInstance<TSelf, TData> : ConditionInstance, IInstance<TData>
    where TSelf : ConditionInstance<TSelf, TData>, new()
    where TData : ConditionData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);
    
    public static TSelf Get(TData data) => _pool.Get(data);
    public override void Release() => _pool.Release((TSelf)this);
    
    public abstract void Setup(TData data);
    public abstract void Dispose();
}