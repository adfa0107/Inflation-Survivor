using System;
using adfa.Utility.Attributes;
using UnityEngine;

namespace adfa.Utility.AI.Steering
{
    [Serializable]
    public class WeightedBehavior
    {
        [SerializeField, SerializeReference, SubclassSelector]
        private ISteeringBehavior behavior;
        
        [SerializeField, Range(0f, 10f)]
        private float weight;

        public Vector2 Calculate(SteeringData data)
        {
            return behavior.Calculate(data) * weight;
        }
    }
}