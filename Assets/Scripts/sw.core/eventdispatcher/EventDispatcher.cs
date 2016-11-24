using System.Collections.Generic;
using Assets.Scripts.sw.core.events;
using UnityEngine.Events;

namespace Assets.Scripts.sw.core.eventdispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<string, BaseEvent> _eventDictionary = new Dictionary<string, BaseEvent>();

        public void AddEventListener(string eventType, UnityAction<object> eventHandler)
        {
            BaseEvent baseEvent = null;
            if (_eventDictionary.TryGetValue(eventType, out baseEvent))
            {
                baseEvent.AddListener(eventHandler);
            }
            else
            {
                baseEvent = new BaseEvent();
                baseEvent.AddListener(eventHandler);
                _eventDictionary.Add(eventType, baseEvent);
            }
        }

        public void RemoveEventListener(string eventType, UnityAction<object> eventHandler)
        {
            BaseEvent baseEvent = null;
            if (_eventDictionary.TryGetValue(eventType, out baseEvent))
            {
                baseEvent.RemoveListener(eventHandler);
            }
        }

        public void DispatchEvent(string eventName, object data = null)
        {
            BaseEvent baseEvent = null;
            if (_eventDictionary.TryGetValue(eventName, out baseEvent))
            {
                baseEvent.Invoke(data);
            }
        }
    }
}
