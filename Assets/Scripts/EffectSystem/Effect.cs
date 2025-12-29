using UnityEngine;

namespace InflationSurvivor.EffectSystem;

public abstract class Effect : ScriptableObject
{
    public abstract void Play(GameObject source);
}