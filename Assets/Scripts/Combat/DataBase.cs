using System.Collections.Generic;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine.Assertions;

namespace InflationSurvivor.Combat;

public static class DataBase<T> where T : class, IHasID
{
    private static readonly Dictionary<string, T> _data = new Dictionary<string, T>();

    public static void Register(T value)
    {
        Assert.IsFalse(_data.ContainsKey(value.ID));
        
        _data.TryAdd(value.ID, value);
    }
    
    public static bool TryGet(string id, out T value) => _data.TryGetValue(id, out value);
    public static bool Contains(string id) => _data.ContainsKey(id);
    
    public static void Clear() => _data.Clear();
}