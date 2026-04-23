using System;
using InflationSurvivor.Combat.Attributes;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

[Serializable]
public class StatusEffectDefinition
{
    [SerializeField] private string id;
    [SerializeField] private string effectName;
    [SerializeField] private Sprite icon;
    [SerializeField] private ExclusiveGroupDefinition exclusiveGroup;
    [SerializeField, SerializeReference, FormulaSelector]
    private IFormula<StatusEffectContext> duration;
}