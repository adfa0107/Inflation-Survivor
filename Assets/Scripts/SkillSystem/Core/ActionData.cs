using System;
using adfa.Utility.Attributes;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public sealed class ActionData
{
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField, SerializeReference, SubclassSelector] public CastData Cast { get; private set; }
    [field: SerializeField, SerializeReference, SubclassSelector] public Effect[] Effects { get; private set; }
}