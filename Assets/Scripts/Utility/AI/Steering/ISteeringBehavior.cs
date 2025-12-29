using UnityEngine;

namespace adfa.Utility.AI.Steering
{
    public interface ISteeringBehavior
    {
        Vector2 Calculate(SteeringData data);
    }
}