using System.Collections.Generic;
using adfa.Utility.AI.Steering;
using InflationSurvivor.CombatSystem.Stat;
using UnityEngine;

namespace InflationSurvivor.CharacterSystem.Monster;

public class MonsterController : MonoBehaviour
{
    private ContactFilter2D _filter;

    [SerializeField] private float speed;
    private float _collisionRadius;
    private Rigidbody2D _rigidbody;
    private SteeringData _steeringData;
    private IReadOnlyList<WeightedBehavior> _behaviors;
    private Monster _monster;

        
    public void Setup(MonsterData data, Transform target)
    {
        _steeringData.target = target;
        _behaviors = data.Behaviors;
    }
        
    private void Awake()
    {
        _filter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Character") | LayerMask.GetMask("Player"),
            useTriggers = false,
            useLayerMask = true,
        };
        _rigidbody = GetComponent<Rigidbody2D>();
        _monster = GetComponent<Monster>();
            
        _steeringData = new SteeringData
        {
            rigidbody = _rigidbody,
            collider = GetComponent<Collider2D>()
        };
    }

    private void FixedUpdate()
    {
        Vector2 finalSteeringForce = Vector2.zero;

        foreach (WeightedBehavior behavior in _behaviors)
        {
            if (behavior != null)
            {
                finalSteeringForce += behavior.Calculate(_steeringData);
            }
        }

        _rigidbody.linearVelocity = finalSteeringForce.normalized * _monster.StatModule.Stat[StatType.Speed];
    }
}