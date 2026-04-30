using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Core.ObjectPool;

namespace InflationSurvivor.StatusEffects;

public abstract class StatusEffectAction
{
    public abstract void Apply(StatusEffectContext context);
    public abstract void Remove();
    public abstract void Update(float deltaTime);

    public abstract void Release();
}

public abstract class StatusEffectAction<TSelf, TData> : StatusEffectAction, IInstance<TData>
    where TSelf : StatusEffectAction<TSelf, TData>, new()
    where TData : StatusEffectActionData
{
    private static readonly InstancePool<TSelf, TData> _pool = new InstancePool<TSelf, TData>(100);

    protected TData data;
    protected CombatModule owner;
    
    public static TSelf Get(TData data) => _pool.Get(data);
    public sealed override void Release() => _pool.Release((TSelf)this);
    
    public void Setup(TData data)
    {
        this.data = data;
        
        OnSetup();
    }

    public void Dispose()
    {
        data = null;
        
        OnDispose();
    }

    public sealed override void Apply(StatusEffectContext context)
    {
        owner = context.target;   
        OnApply(context);
    }

    public sealed override void Remove()
    {
        OnRemove();
        owner = null;
    }

    protected abstract void OnSetup();
    protected abstract void OnDispose();
    protected abstract void OnApply(StatusEffectContext context);
    protected abstract void OnRemove();
}