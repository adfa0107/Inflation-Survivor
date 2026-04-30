using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public class StatusEffectInfo
{
    public readonly string id;
    public readonly string name;
    public readonly Sprite icon;
    public readonly int priority;

    public readonly IExclusiveGroup exclusiveGroup;
    public readonly IFormula<StatusEffectContext> duration;
    public readonly IFormula<StatusEffectContext> initStack;
    public readonly IFormula<StatusEffectContext> maxStack;

    public StatusEffectInfo(string id, string name, Sprite icon, int priority, IExclusiveGroup exclusiveGroup,
        IFormula<StatusEffectContext> duration, IFormula<StatusEffectContext> initStack,
        IFormula<StatusEffectContext> maxStack)
    {
        this.id = id;
        this.name = name;
        this.icon = icon;
        this.priority = priority;
        this.exclusiveGroup = exclusiveGroup;
        this.duration = duration;
        this.initStack = initStack;
        this.maxStack = maxStack;
    }
}