using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InflationSurvivor.Core.ObjectPool;
using InflationSurvivor.EventSystem;
using UnityEngine.Assertions;

namespace InflationSurvivor.SkillSystem.Core;

public sealed class ActionInstance : IInstance<ActionDefinition>
{
    private static readonly InstancePool<ActionInstance, ActionDefinition> _pool = new(100);

    private float _delay;
    private CastInstance _cast;
    private IReadOnlyList<SkillEffect> _effects;

    public static ActionInstance Get(ActionDefinition data) => _pool.Get(data);
    public void Release() => _pool.Release(this);

    public void Setup(ActionDefinition data)
    {
        Assert.IsNotNull(data.Cast);
        
        _delay = data.Delay;
        _cast = data.Cast.CreateInstance();
        _effects = data.Effects;
    }

    public void Dispose()
    {
        _cast.Release();
        _cast = null;
        _effects = null;
    }

    public async UniTaskVoid Execute(SkillCastModule caster, GameEvent @event)
    {
        await UniTask.WaitForSeconds(_delay);
        _cast.Cast(caster, SkillEffectPackage.Get(caster, @event, _effects));
    }
}