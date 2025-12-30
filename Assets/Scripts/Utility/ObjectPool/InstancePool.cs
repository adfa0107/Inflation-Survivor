namespace adfa.Utility.ObjectPool;

public class InstancePool<T, TData> : PoolBase<T> where T : class, IInstance<TData>, new()
{
    public InstancePool(int capacity, int initialSize = 0) : base(capacity, initialSize) { }

    public T Get(TData data)
    {
        T item = Pop();
        item.Setup(data);
        return item;
    }

    public void Release(T item)
    {
        item.Reset();
        Push(item);
    }
}