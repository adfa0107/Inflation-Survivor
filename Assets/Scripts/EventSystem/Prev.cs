using adfa.Utility.ObjectPool;

namespace InflationSurvivor.EventSystem;

public sealed class Prev<T> : GameEvent where T : struct
{
    private static readonly SimplePool<Prev<T>> _pool = new SimplePool<Prev<T>>(100);
    
    public T data;
    public bool IsCancelled { get; private set; }

    public static Prev<T> Get(T data)
    {
        Prev<T> postData = _pool.Get();
        postData.data = data;
        postData.IsCancelled = false;
        return postData;
    }

    public void Cancel()
    {
        IsCancelled = true;
    }

    public void Release() => _pool.Release(this);
}