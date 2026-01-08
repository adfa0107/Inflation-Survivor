using System;
using InflationSurvivor.Core.ObjectPool;

namespace InflationSurvivor.EventSystem;

public sealed class Prev<T> : GameEvent, IDisposable where T : struct, IEvent
{
    private static readonly SimplePool<Prev<T>> _pool = new SimplePool<Prev<T>>(100);
    
    public T data;
    public bool IsCancelled { get; private set; }
    private bool _cancelable;

    public static Prev<T> Get(T data, bool cancelable)
    {
        Prev<T> postData = _pool.Get();
        postData.data = data;
        postData.IsCancelled = false;
        postData._cancelable = cancelable;
        return postData;
    }

    public void Cancel()
    {
        IsCancelled = !_cancelable;
    }

    public void Release() => _pool.Release(this);

    public void Dispose()
    {
        
    }
}