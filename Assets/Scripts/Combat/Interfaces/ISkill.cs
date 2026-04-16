using System.Collections.Generic;
using InflationSurvivor.Combat.Contexts;

namespace InflationSurvivor.Combat.Interfaces;

public interface ISkill : IHasID
{
    public int Level { get; }
    public float Cooldown { get; }
    public bool CanUse { get; }
    
    public IReadOnlyCollection<string> Tags { get; }
    
    public void Execute(CombatModule target);
}