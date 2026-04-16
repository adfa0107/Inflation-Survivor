using System;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.Combat.Abstractions;

[Serializable]
public abstract class SkillDefinitionBase : ScriptableObject
{
    public abstract ISkillData CreateData();
}