using System;

namespace InflationSurvivor.CombatData.ResourceSystem;

[Serializable]
public struct ResourceStat
{
    public float max;
    public float fixedRegeneration;
    public float ratioRegeneration;
    
    public float Regeneration => fixedRegeneration + max * ratioRegeneration;
}