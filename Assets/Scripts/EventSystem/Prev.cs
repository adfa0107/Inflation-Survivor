using adfa.Utility.ObjectPool;

namespace InflationSurvivor.EventSystem;

public sealed class Prev<T> : GameEventData where T : struct
{
    private static readonly SimplePool<Prev<T>> _pool = new SimplePool<Prev<T>>(100);
    
    public T data;
    public bool isCancelled;

    public static Prev<T> Get(T data)
    {
        Prev<T> postData = _pool.Get();
        postData.data = data;
        postData.isCancelled = false;
        return postData;
    }

    public void Release() => _pool.Release(this);
}