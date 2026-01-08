using System;
using SerializeReferenceEditor;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public sealed class ActionDefinition
{
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField, SerializeReference, SR(typeof(CastDefinition))] public CastDefinition Cast { get; private set; }
    [field: SerializeField, SerializeReference, SR(typeof(SkillEffect))] public SkillEffect[] Effects { get; private set; }
}