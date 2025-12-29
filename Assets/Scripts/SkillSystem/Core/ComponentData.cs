using System;
using adfa.Utility.Attributes;
using UnityEngine;

namespace InflationSurvivor.SkillSystem.Core;

[Serializable]
public sealed class ComponentData
{
    [field: SerializeField, SerializeReference, SubclassSelector] public ConditionData[] Conditions { get; private set; }
    [field: SerializeField] public ActionData[] Actions { get; private set; }
}