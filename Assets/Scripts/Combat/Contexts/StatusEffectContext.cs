using InflationSurvivor.Combat.Interfaces;
using InflationSurvivor.Combat.Interfaces.StatusEffect;

namespace InflationSurvivor.Combat.Contexts;

public struct StatusEffectContext
{
    public CombatModule source;
    public CombatModule target;
    public IFormula<StatusEffectContext> overrideDuration;
}