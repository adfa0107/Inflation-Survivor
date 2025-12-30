using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace adfa.Utility.ObjectPool;

public class PoolBase<T> where T : class, new()
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
        
        if (_count < _capacity)
        {
#if UNITY_EDITOR
            for (int i = 0; i < _count; i++)
            {
                if (_array[i] == item)
                {
                    Debug.LogError($"{typeof(T)}객체를 반환하는 과정에서 중복 반환이 일어났습니다.");
                    return;
                }
            }
#endif
            _array[_count++] = item;
        }
    }
}