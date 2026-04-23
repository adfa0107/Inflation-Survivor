using System;
using InflationSurvivor.Combat.Interfaces;

namespace InflationSurvivor.StatusEffects;

public class ExclusiveGroup : IHasID, IEquatable<ExclusiveGroup>
{
    public readonly BaseSelectPolicy @base;
    public readonly MergePolicy duration;
    public readonly MergePolicy stack;
    
    public string ID { get; }

    public ExclusiveGroup(string id, BaseSelectPolicy @base, MergePolicy duration, MergePolicy stack)
    {
        ID = id;
        this.@base = @base;
        this.duration = duration;
        this.stack = stack;
    }

    public bool Equals(ExclusiveGroup other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ID == other.ID;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ExclusiveGroup)obj);
    }

    public override int GetHashCode()
    {
        return (ID != null ? ID.GetHashCode() : 0);
    }
}