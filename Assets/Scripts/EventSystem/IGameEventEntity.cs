using System;
using InflationSurvivor.SkillSystem.Core;

namespace InflationSurvivor.EventSystem;

public interface IGameEventEntity
{
    public void SubscribePreEvent(GameEventType eventType, Action<SkillContext> callback);
    public void SubscribePostEvent(GameEventType eventType, Action<SkillContext> callback);
    public void UnsubscribePreEvent(GameEventType eventType, Action<SkillContext> callback);
    public void UnsubscribePostEvent(GameEventType eventType, Action<SkillContext> callback);
    public void Raise(GameEventType eventType, SkillContext eventData);
}