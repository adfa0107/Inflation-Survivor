namespace adfa.Utility.ObjectPool;

public class InstancePool<T, TData> : SimplePool<T> where T : class, IInstance<TData>, new()
{
    public InstancePool(int initialSize = 0) : base(initialSize) { }

    public T Get(TData data)
    {
        T instance = Get();
        instance.Create(data);
        return instance;
    }
}