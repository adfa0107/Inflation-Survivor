using System;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Abstractions;

[Serializable]
public abstract class StatusEffectDefinition : ScriptableObject
{
    public abstract IStatusEffectData Build();
}