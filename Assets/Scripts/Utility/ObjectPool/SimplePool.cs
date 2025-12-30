namespace adfa.Utility.ObjectPool;

public class SimplePool<T> : PoolBase<T> where T : class, new()
{
    public SimplePool(int capacity, int initialSize = 0) : base(capacity, initialSize) { }

    public T Get()
    {
        return Pop();
    }

    public void Release(T item)
    {
        Push(item);
    }
}