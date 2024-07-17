using System;



namespace TrapTheCat.Events
{
    public class EventController<T>
    {
        // Event of type T
        public event Action<T> baseEvent;

        // Invokes the event, passing a parameter of type T
        public void InvokeEvent(T type) => baseEvent?.Invoke(type);

        // Adds a listener to the event
        public void AddListener(Action<T> listener) => baseEvent += listener;

        // Removes a listener from the event
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }
}

