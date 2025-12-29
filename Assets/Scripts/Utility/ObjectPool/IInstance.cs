namespace adfa.Utility.ObjectPool;

public interface IInstance<in TData>
{
    public void Create(TData data);
}