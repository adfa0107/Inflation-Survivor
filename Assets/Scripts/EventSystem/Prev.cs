namespace InflationSurvivor.EventSystem;

public sealed class Prev<T> : GameEventData where T : struct
{
    public T data;
    public bool isCancelled;
}