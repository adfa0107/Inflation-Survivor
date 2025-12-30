namespace adfa.Utility.ObjectPool;

public interface IInstance<in TData>
{
    public void Setup(TData data);
    public void Reset();
}