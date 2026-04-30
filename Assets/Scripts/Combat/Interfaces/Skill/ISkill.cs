using System.Collections.Generic;

namespace InflationSurvivor.Combat.Interfaces.Skill;

public interface ISkill : IHasID
{
    public int Level { get; }
    public float Cooldown { get; }
    public bool CanUse { get; }
    
    public IReadOnlyCollection<string> Tags { get; }
    
    public void Execute(CombatModule target);
}