using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine.Assertions;

namespace InflationSurvivor.SkillSystem;

public sealed class SkillInstance : IInstance<SkillData>
{
    private static readonly InstancePool<SkillInstance, SkillData> _pool = new();
    private readonly List<ComponentInstance> _components = new List<ComponentInstance>();

    public static SkillInstance Get(SkillData data) => _pool.Get(data);
        
    public void Create(SkillData data)
    {
        foreach (ComponentData component in data.Components)
        {
            Assert.IsNotNull(component);
            _components.Add(ComponentInstance.Get(component));
        }
    }

    public void Reset()
    {
        foreach (ComponentInstance component in _components)
        {
            component.Reset();
        }
    }

    public void Release()
    {
        foreach (ComponentInstance component in _components)
        {
            component.Release();
        }
        _components.Clear();
        _pool.Release(this);
    }

    public void Execute(SkillContext context)
    {
        foreach (ComponentInstance component in _components)
        {
            component.Execute(context);
        }
    }
}