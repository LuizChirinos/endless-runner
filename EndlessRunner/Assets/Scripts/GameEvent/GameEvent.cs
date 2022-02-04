using System;
using UnityEngine;

namespace Triplano
{
    [CreateAssetMenu(fileName = nameof(GameEvent), menuName = "GameEvents/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public Action OnTriggeredEvent;

        public void SubscribeToEvent(Action action)
        {
            OnTriggeredEvent += action;
        }
        public void UnsubscribeToEvent(Action action)
        {
            OnTriggeredEvent -= action;
        }

        public void TriggerEvent() => OnTriggeredEvent?.Invoke();
    }
}
