using System;
using System.Collections.Generic;
using InflationSurvivor.Core;

namespace InflationSurvivor.EventSystem;

public sealed class EventModule
{
    private readonly Dictionary<Type, Action<GameEventData>> _handlers = new Dictionary<Type, Action<GameEventData>>();

    public void OnDisable()
    {
        _handlers.Clear();
    }

    public void SubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData
    {
        Type type = typeof(TEventData);
        
        Action<GameEventData> handler = _handlers.GetValueOrDefault(type, null);
        handler += callback;
        _handlers[type] = handler;
    }

    public void UnsubscribeEvent<TEventData>(Action<GameEventData> callback) where TEventData : GameEventData
    {
        Type type = typeof(TEventData);
        
        Action<GameEventData> handler = _handlers.GetValueOrDefault(type, null);
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

    public void Raise<TEventData>(TEventData eventData) where TEventData : GameEventData
    {
        _handlers.GetValueOrDefault(typeof(TEventData))?.Invoke(eventData);
    }
}