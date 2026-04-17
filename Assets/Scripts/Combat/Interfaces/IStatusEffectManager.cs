namespace InflationSurvivor.Combat.Interfaces;

public interface IStatusEffectManager
{
    public bool HasEffect(string id);

    public void DeleteEffectsByID(string id);
    public void DeleteEffectsByTag(string tag);
}