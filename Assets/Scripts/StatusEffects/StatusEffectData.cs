using InflationSurvivor.Combat.Contexts;
using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;

namespace InflationSurvivor.StatusEffects;

public sealed class StatusEffectData : IStatusEffectData
{
    private readonly StatusEffectInfo _info;
    public readonly StatusEffectActionData[] actions;

    public string ID => _info.id;
    public int Priority => _info.priority;
    public IExclusiveGroup ExclusiveGroup => _info.exclusiveGroup;
    public IFormula<StatusEffectContext> Duration => _info.duration;
    public IFormula<StatusEffectContext> InitStack => _info.initStack;
    public IFormula<StatusEffectContext> MaxStack => _info.maxStack;

    public StatusEffectData(StatusEffectInfo info, StatusEffectActionData[] actions)
    {
        _info = info;
        this.actions = actions;
    }

    public IStatusEffect Create()
    {
        return StatusEffect.Get(this);
    }
}