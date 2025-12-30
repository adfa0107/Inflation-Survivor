using System.Collections.Generic;
using InflationSurvivor.CombatSystem;
using InflationSurvivor.CombatSystem.Stat;
using InflationSurvivor.SkillSystem.Core;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Casts;

public class NearestTargetsCastInstance : CastInstance<NearestTargetsCastInstance, NearestTargetsCastData>
{
    private ContactFilter2D _contactFilter;
        
    private bool _bIsIncludeSelf;
    private ScaledValue _angle;
    private ScaledValue _minRadius;
    private ScaledValue _maxRadius;
    private ScaledValue _maxTargetCount;
    private TargetFaction _targetFaction;
    private readonly List<Collider2D> _colliders = new List<Collider2D>();
    private readonly List<CombatModule> _targets = new List<CombatModule>();
    
    public override void Setup(NearestTargetsCastData data)
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

    public override void Reset()
    {
        _colliders.Clear();
        _targets.Clear();
    }

    public override void Cast(SkillCastModule caster, SkillEffectPackage effectPackage)
    {
        Vector2 origin = caster.transform.position;
        Vector2 forward = caster.transform.forward;
        
        float halfAngle = _angle.GetScaledValue(caster.StatModule) * 0.5f;
        float squareMinRadius = _minRadius.GetScaledValue(caster.StatModule);
        int maxTargetCount = _maxTargetCount.GetScaledValueAsInt(caster.StatModule);
        
        bool bIsNotSector = halfAngle >= 180f;
        squareMinRadius *= squareMinRadius;
        
        Physics2D.OverlapCircle(origin, _maxRadius.GetScaledValue(caster.StatModule), _contactFilter, _colliders);
        
        foreach (Collider2D collider in _colliders)
        {
            Vector2 offset = (Vector2)collider.transform.position - origin;
            
            if (offset.sqrMagnitude > squareMinRadius && 
                (bIsNotSector || Vector2.Angle(forward, offset.normalized) < halfAngle) && 
                CombatModule.TryGetModule(collider, out CombatModule target))
            {
                _targets.Add(target);
            }
        }
        
        int removeTargetCount = _targets.Count - maxTargetCount;

        for (int i = 0; i < removeTargetCount; i++)
        {
            int index = Random.Range(0, _targets.Count);
            _targets.RemoveAt(index);
        }
        
        effectPackage.Apply(_targets);
        
        _colliders.Clear();
        _targets.Clear();
    }
}