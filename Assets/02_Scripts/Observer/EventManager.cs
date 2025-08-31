using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<string, UnityEvent> eventList = new();

    public void Trigger(string eventName)
    {
        if (!eventList.ContainsKey(eventName))
        {
            return;
        }

        eventList[eventName]?.Invoke();
    }

    public void Subscribe(string eventName, UnityAction eventHandler)
    {
        if (!eventList.ContainsKey(eventName))
        {
            eventList[eventName] = new UnityEvent();
        }

        eventList[eventName].AddListener(eventHandler);
    }

    public void Unsubscribe(string eventName, UnityAction eventHandler)
    {
        if (!eventList.ContainsKey(eventName))
        {
            return;
        }

        eventList[eventName].RemoveListener(eventHandler);
    }
}
