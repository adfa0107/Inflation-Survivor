using System;
using SerializeReferenceEditor;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public sealed class ActionData
{
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField, SerializeReference, SR(typeof(CastData))] public CastData Cast { get; private set; }
    [field: SerializeField, SerializeReference, SR(typeof(SkillEffect))] public SkillEffect[] Effects { get; private set; }
}