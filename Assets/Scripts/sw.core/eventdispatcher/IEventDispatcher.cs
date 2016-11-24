using UnityEngine.Events;

namespace Assets.Scripts.sw.core.eventdispatcher
{
    public interface IEventDispatcher
    {
        void AddEventListener(string eventType, UnityAction<object> eventHandler);

        void RemoveEventListener(string eventType, UnityAction<object> eventHandler);

        void DispatchEvent(string eventName, object data = null);
    }
}
