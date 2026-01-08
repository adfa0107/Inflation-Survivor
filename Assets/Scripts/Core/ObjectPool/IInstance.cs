using System;

namespace InflationSurvivor.Core.ObjectPool;

public interface IInstance<in TData> : IDisposable
{
    public void Setup(TData data);
}