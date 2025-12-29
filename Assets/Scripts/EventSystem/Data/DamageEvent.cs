using UnityEngine;

namespace InflationSurvivor.EventSystem.Data;

public struct DamageEvent
{
    public GameObject attacker;
    public GameObject target;
    
    public float damage;
}