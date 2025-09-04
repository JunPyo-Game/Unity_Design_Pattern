using System.Collections.Generic;
using UnityEngine.Events;

static public class EventBus
{
    static private readonly Dictionary<string, UnityEvent> eventList = new();

    static public void Trigger(string eventName)
    {
        if (!eventList.ContainsKey(eventName))
        {
            return;
        }

        eventList[eventName]?.Invoke();
    }

    static public void Subscribe(string eventName, UnityAction eventHandler)
    {
        if (!eventList.ContainsKey(eventName))
        {
            eventList[eventName] = new UnityEvent();
        }

        eventList[eventName].AddListener(eventHandler);
    }

    static public void Unsubscribe(string eventName, UnityAction eventHandler)
    {
        if (!eventList.ContainsKey(eventName))
        {
            return;
        }

        eventList[eventName].RemoveListener(eventHandler);
    }
}
