using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.Combat.Contexts;

public struct StatusEffectContext
{
    public CombatModule owner;
    public CombatModule target;
    public IStatusEffect statusEffect;
    public IFormula<StatusEffectContext> overrideDuration;
}