using System.Collections.Generic;

namespace adfa.Utility.ObjectPool;

public class InstancePool<T, TData> where T : class, IInstance<TData>, new()
{
    private readonly Stack<T> _stack = new Stack<T>();
    
    public InstancePool(int initialSize = 0)
    {
        for (int i = 0; i < initialSize; i++)
        {
            _stack.Push(new T());
        }
    }

    public T Get(TData data)
    {
        T instance = _stack.Count > 0 ? _stack.Pop() : new T();
        instance.Create(data);
        return instance;
    }
    
    public void Release(T obj)
    {
        _stack.Push(obj);
    }
}