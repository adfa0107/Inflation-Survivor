using System;
using UnityEngine;

namespace adfa.Utility.AI.Steering.Behaviors
{
    [Serializable]
    public class SeekBehavior : ISteeringBehavior
    {
        public Vector2 Calculate(SteeringData data)
        {
            if (data.target is null)
            {
                return Vector2.zero;
            }
            
            Vector2 directionToTarget = (Vector2)data.target.position - data.rigidbody.position;
            return directionToTarget.normalized;
        }
    }
}