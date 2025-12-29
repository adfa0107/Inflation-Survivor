using System.Collections.Generic;
using System.Linq;
using InflationSurvivor.SkillSystem.Core;
using InflationSurvivor.SkillSystem.Interfaces;
using InflationSurvivor.StatSystem;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Casts;

public class NearestTargetsCastInstance : CastInstance<NearestTargetsCastInstance, NearestTargetsCastData>
{
    private ContactFilter2D _contactFilter;
        
    private bool _bIsIncludeSelf;
    private bool _bIsSector;
    private ScaledValue _angle;
    private ScaledValue _minRadius;
    private ScaledValue _maxRadius;
    private ScaledValue _maxTargetCount;
    private TargetFaction _targetFaction;
    private readonly List<Collider2D> _colliders = new List<Collider2D>();
    private readonly List<ISkillTarget> _skillTargets = new List<ISkillTarget>();
    
    public override void Create(NearestTargetsCastData data)
    {
        _contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = LayerMask.GetMask("Player") | LayerMask.GetMask("Character")
        };
        _bIsIncludeSelf = data.bIsIncludeSelf;
        _angle = data.Angle;
        _minRadius = data.MinRadius;
        _maxRadius = data.MaxRadius;
        _maxTargetCount = data.MaxTargetCount;
        _targetFaction = data.TargetFaction;
    }

    public override void Release()
    {
        _colliders.Clear();
        _skillTargets.Clear();
        base.Release();
    }

    public override void Cast(SkillContext context, SkillEffectPackage effectPackage)
    {
        Vector2 origin = context.caster.Transform.position;
        Vector2 forward = context.caster.Transform.forward;
        float halfAngle = _angle.GetScaledValue(context.caster) * 0.5f;
        float squareMinRadius = _minRadius.GetScaledValue(context.caster);
        squareMinRadius *= squareMinRadius;
        bool bIsNotSector = halfAngle >= 180f;
        int maxTargetCount = _maxTargetCount.GetScaledValueAsInt(context.caster);
        
        Physics2D.OverlapCircle(origin, _maxRadius.GetScaledValue(context.caster), _contactFilter, _colliders);
        
        foreach (Collider2D collider in _colliders)
        {
            Vector2 offset = (Vector2)collider.transform.position - origin;
            
            if (offset.sqrMagnitude > squareMinRadius && 
                (bIsNotSector || Vector2.Angle(forward, offset.normalized) < halfAngle) && 
                SkillTargetCache.TryGetSkillTarget(collider, out ISkillTarget skillTarget))
            {
                _skillTargets.Add(skillTarget);
            }
        }
        
        int removeTargetCount = _skillTargets.Count - maxTargetCount;

        for (int i = 0; i < removeTargetCount; i++)
        {
            int index = Random.Range(0, _skillTargets.Count);
            _skillTargets.RemoveAt(index);
        }
        
        effectPackage.Apply(_skillTargets);
        
        _colliders.Clear();
        _skillTargets.Clear();
    }
}