using System;
using adfa.Utility.ObjectPool;

namespace InflationSurvivor.EventSystem;

public class Post<T> : GameEventData where T : struct
{
    private static readonly SimplePool<Post<T>> _pool = new SimplePool<Post<T>>(100);
    
    public T Data { get; private set; }

    public static Post<T> Get(T data)
    {
        Post<T> postData = _pool.Get();
        postData.Data = data;
        return postData;
    }

    public void Release() => _pool.Release(this);
}