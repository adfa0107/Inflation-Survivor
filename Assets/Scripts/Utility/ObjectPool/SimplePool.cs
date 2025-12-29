using System;
using System.Collections.Generic;

namespace adfa.Utility.ObjectPool;

public class SimplePool<T> where T : class, new()
{
    private readonly Stack<T> _stack = new Stack<T>();
    
    public SimplePool(int initialSize = 0)
    {
        for (int i = 0; i < initialSize; i++)
        {
            _stack.Push(new T());
        }
    }

    public T Get()
    {
        return _stack.Count > 0 ? _stack.Pop() : new T();
    }
    
    public void Release(T obj)
    {
        _stack.Push(obj);
    }
}