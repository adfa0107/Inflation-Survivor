namespace InflationSurvivor.EventSystem;

public class Post<T> : GameEventData where T : struct
{
    public T Data { get; set; }
}