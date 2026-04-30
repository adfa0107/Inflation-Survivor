namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IStatusEffectValueSelector
{
    public int Select(int @base, int old, int @new);
    public float Select(float @base, float old, float @new);
}