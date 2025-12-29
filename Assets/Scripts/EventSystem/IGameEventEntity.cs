using System;

namespace InflationSurvivor.EventSystem;

public interface IGameEventEntity
{
    public void SubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData;
    public void UnsubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData;
    public void Raise<TEventData>(TEventData eventData) where TEventData : GameEventData;
}