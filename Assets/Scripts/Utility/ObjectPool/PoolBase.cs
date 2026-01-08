using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace adfa.Utility.ObjectPool;

public class PoolBase<T> where T : class, IDisposable, new()
{
    private readonly T[] _array;
    private int _count;
    private readonly int _capacity;

    public PoolBase(int capacity, int initialSize = 0)
    {
        initialSize = Mathf.Min(initialSize, capacity);
        _capacity = capacity;
        _array = new T[capacity];

        for (int i = 0; i < initialSize; i++)
        {
            _array[i] = new T();
        }

        _count = initialSize;
    }

    protected T Pop()
    {
        T item;
        
        if (_count > 0)
        {
            _count--;
            item = _array[_count];
            _array[_count] = null;
        }
        else
        {
            item = new T();
        }
        
        return item;
    }

    protected void Push(T item)
    {
        Assert.IsNotNull(item);

        if (item is null)
        {
            return;
        }
        
        item.Dispose();
        
        if (_count < _capacity)
        {
#if UNITY_EDITOR
            for (int i = 0; i < _count; i++)
            {
                Assert.IsTrue(_array[i] != item);
            }
#endif
            _array[_count++] = item;
        }
    }
}