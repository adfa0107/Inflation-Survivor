using System;
using System.Collections.Generic;

namespace InflationSurvivor.EventSystem;

public sealed class EventHandler
{
    private readonly Dictionary<Type, Action<GameEvent>> _handlers = new Dictionary<Type, Action<GameEvent>>();

    public void OnDisable()
    {
        _handlers.Clear();
    }

    public void SubscribeEvent<TEventData>(Action<GameEvent> callback) where TEventData : GameEvent
    {
        Type type = typeof(TEventData);
        
        Action<GameEvent> handler = _handlers.GetValueOrDefault(type, null);
        handler += callback;
        _handlers[type] = handler;
    }

    public void UnsubscribeEvent<TEventData>(Action<GameEvent> callback) where TEventData : GameEvent
    {
        Type type = typeof(TEventData);
        
        Action<GameEvent> handler = _handlers.GetValueOrDefault(type, null);
        handler -= callback;
        if (handler is null)
        {
            _handlers.Remove(type);
        }
        else
        {
            _handlers[type] = handler;
        }
    }

    public void Raise<TEventData>(TEventData eventData) where TEventData : GameEvent
    {
        _handlers.GetValueOrDefault(typeof(TEventData))?.Invoke(eventData);
    }
}