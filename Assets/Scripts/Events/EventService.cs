

using UnityEngine;

namespace TrapTheCat.Events
{
    public class EventService
    {
        // Event triggered when the game starts
        public EventController<bool> OnGameStart { get; private set; }

        // Event triggered when the game is over
        public EventController<bool> OnGameOver { get; private set; }

        // Event triggered when a cell is blocked
        public EventController<Vector2Int> OnBlockCell { get; private set; }

        // Constructor to initialize the event controllers
        public EventService()
        {
            OnGameStart = new EventController<bool>();
            OnGameOver = new EventController<bool>();
            OnBlockCell = new EventController<Vector2Int>();
        }

    }
}