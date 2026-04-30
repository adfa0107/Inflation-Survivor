using System;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.Combat.Abstractions;

[Serializable]
public abstract class StatusEffectDefinitionBase : ScriptableObject
{
    public abstract IStatusEffectData Build();
}