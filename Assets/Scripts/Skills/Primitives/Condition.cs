using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Core.ObjectPool;

namespace InflationSurvivor.Skills.Primitives;

public abstract class Condition
{
    public abstract void Release();

    public abstract bool CanActivate(SkillContext context);
    public abstract void Deactivate(SkillContext context);
    public abstract void Update(SkillContext context);
    
    
    protected abstract bool IsActive();
    
    public static implicit operator bool(Condition instance)
    {
        return instance is not null && instance.IsActive();
    }
}

public abstract class Condition<TSelf, TData> : Condition, IInstance<TData>
    where TSelf : Condition<TSelf, TData>, new()
    where TData : ConditionData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);

    protected TData data;
    
    public static TSelf Get(TData data) => _pool.Get(data);
    public override void Release() => _pool.Release((TSelf)this);

    public void Setup(TData data)
    {
        this.data = data;
        Setup();
    }
    
    protected abstract void Setup();
    public abstract void Dispose();
}