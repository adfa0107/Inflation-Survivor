using InflationSurvivor.Combat;
using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;
using UnityEngine;

namespace InflationSurvivor.StatusEffects;

public abstract class StatusEffectData : IStatusEffectData
{
    public readonly string name;
    public readonly Sprite icon;
    public readonly IFormula<StatusEffectContext> maxStack;
    
    public string ID { get; }
    public int Priority { get; }
    public IExclusiveGroup ExclusiveGroup { get; }
    public IFormula<StatusEffectContext> InitStack { get; }
    public IFormula<StatusEffectContext> Duration { get; }

    protected StatusEffectData(string id, string name, Sprite icon, int priority, IExclusiveGroup exclusiveGroup,
        IFormula<StatusEffectContext> duration, IFormula<StatusEffectContext> initStack, IFormula<StatusEffectContext> maxStack)
    {
        ID = id;
        this.name = name;
        this.icon = icon;
        Priority = priority;
        ExclusiveGroup = exclusiveGroup;
        Duration = duration;
        InitStack = initStack;
        this.maxStack = maxStack;
    }

    public abstract IStatusEffect Create();
}