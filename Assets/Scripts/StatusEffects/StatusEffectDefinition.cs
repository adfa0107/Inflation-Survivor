using System;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public class StatusEffectDefinition
{
    [SerializeField] private string id;
    [SerializeField] private string effectName;
    [SerializeField] private Sprite icon;
}