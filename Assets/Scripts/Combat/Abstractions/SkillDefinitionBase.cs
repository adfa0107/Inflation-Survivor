using System;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.Skill;
using UnityEngine;

namespace InflationSurvivor.Combat.Abstractions;

[Serializable]
public abstract class SkillDefinitionBase : ScriptableObject
{
    public abstract ISkillData Build();
}