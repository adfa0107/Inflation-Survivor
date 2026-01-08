using System;

namespace adfa.Utility.ObjectPool;

public interface IInstance<in TData> : IDisposable
{
    public void Setup(TData data);
}