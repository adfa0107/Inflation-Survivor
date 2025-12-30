using UnityEngine;

namespace InflationSurvivor.EventSystem.Data;

public struct DamageEvent
{
    public IGameEventEntity attacker;
    public IGameEventEntity target;
    
    public float damage;
}