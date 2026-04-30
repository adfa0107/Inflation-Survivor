namespace InflationSurvivor.Combat.Contexts;

public struct StatusEffectContext
{
    public CombatModule source;
    public CombatModule target;

    public int stack;

    public float predefinedDuration;
    public float predefinedInitStack;
    public float predefinedMaxStack;
}