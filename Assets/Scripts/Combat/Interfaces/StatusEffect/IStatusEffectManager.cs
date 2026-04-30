namespace InflationSurvivor.Combat.Interfaces.StatusEffect;

public interface IStatusEffectManager
{
    public bool Has(string id);
    
    public void Add(IStatusEffectData effect, CombatModule source);

    public void DeleteByID(string id);
    public void DeleteByTag(string tag);
}