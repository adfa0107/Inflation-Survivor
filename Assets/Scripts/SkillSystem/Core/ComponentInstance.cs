using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using Assert = UnityEngine.Assertions.Assert;

namespace InflationSurvivor.SkillSystem.Core;

public sealed class ComponentInstance : IInstance<ComponentData>
{
    private static readonly InstancePool<ComponentInstance, ComponentData> _pool = new InstancePool<ComponentInstance, ComponentData>();
    
    private readonly List<ActionInstance> _actions = new List<ActionInstance>();
    private readonly List<ConditionInstance> _conditions = new List<ConditionInstance>();
    
    public static ComponentInstance Get(ComponentData data) => _pool.Get(data);
    
    public void Create(ComponentData data)
    {
        Assert.IsTrue(_actions.Count == 0);
        Assert.IsTrue(_conditions.Count == 0);
        
        foreach (ActionData action in data.Actions)
        {
            Assert.IsNotNull(action);
            _actions.Add(ActionInstance.Get(action));
        }
        
        foreach (ConditionData condition in data.Conditions)
        {
            Assert.IsNotNull(condition);
            _conditions.Add(condition.CreateInstance());
        }
    }

    public void Reset()
    {
        foreach (ConditionInstance condition in _conditions)
        {
            condition.Reset();
        }
    }

    public void Release()
    {
        foreach (ActionInstance action in _actions)
        {
            action.Release();
        }
        _actions.Clear();
        
        foreach (ConditionInstance condition in _conditions)
        {
            condition.Release();
        }
        _conditions.Clear();
        
        _pool.Release(this);
    }

    public void Execute(SkillContext context)
    {
        bool bIsConditionsMet = true;
        foreach (ConditionInstance condition in _conditions)
        {
            bIsConditionsMet &= condition.IsActive(context.caster);
        }

        if (!bIsConditionsMet)
        {
            return;
        }
        
        foreach (ActionInstance action in _actions)
        {
            action.Execute(context).Forget();
        }

        foreach (ConditionInstance condition in _conditions)
        {
            condition.Deactivate(context.caster);
        }
    }
}