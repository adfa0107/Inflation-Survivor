using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace InflationSurvivor.Core;

[Serializable]
public struct LeveledData<T>
{
    [SerializeField] private T[] data;

    public ref T this[int level]
    {
        get
        {
            Assert.IsTrue(0 <= level && level < data.Length);
            return ref data[level];
        }
    }
}