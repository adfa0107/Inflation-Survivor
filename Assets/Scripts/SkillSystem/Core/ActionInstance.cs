using System.Collections.Generic;
using adfa.Utility.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

namespace InflationSurvivor.SkillSystem.Core;

public sealed class ActionInstance : IInstance<ActionData>
{
    private static readonly InstancePool<ActionInstance, ActionData> _pool = new();

    private float _delay;
    private CastInstance _cast;
    private IReadOnlyList<SkillEffect> _effects;

    public static ActionInstance Get(ActionData data) => _pool.Get(data);

    public void Create(ActionData data)
    {
        Assert.IsNotNull(data.Cast);
        _delay = data.Delay;
        _cast = data.Cast.CreateInstance();
        _effects = data.Effects;
    }

    public void Release()
    {
        _cast.Release();
        _cast = null;
        _effects = null;
        
        _pool.Release(this);
    }

    public async UniTask Execute(SkillContext context)
    {
        await UniTask.WaitForSeconds(_delay);
        _cast.Cast(context, SkillEffectPackage.Get(context, _effects));
    }
}