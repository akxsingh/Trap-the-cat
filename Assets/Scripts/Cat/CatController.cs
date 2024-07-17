
using TrapTheCat.Events;
using TrapTheCat.Grid;
using UnityEngine;

namespace TrapTheCat.Cat
{
    public class CatController
    {

        private CatView catView; // Reference to the CatView object
        public Vector2Int position; // Current grid position of the cat
        private EventService eventService; // Service for handling events
        private GridService gridService; // Service for handling grid operations
        public bool isMoving; // Indicates if the cat is currently moving
        public Vector3 targetPosition; // Target position for the cat's movement
        private float moveSpeed = 5f; // Speed at which the cat moves


        // Constructor to initialize the CatController with a CatView prefab and a grid position
        public CatController(CatView catPrefab, Vector2Int position)
        {
            catView = UnityEngine.Object.Instantiate(catPrefab);
            catView.SetController(this);
            this.position = position;
        }

        // Initializes the CatController with event and grid services
        public void Init(EventService eventService, GridService gridService)
        {
            this.eventService = eventService;
            this.gridService = gridService;
        }

        // Sets the target position for the cat to move to and starts the movement
        public void SetPosition(Vector3 positionToSet)
        {
          
            targetPosition = positionToSet;
            isMoving = true;
            catView.isMoving = isMoving;
            catView.SetMoveAnimation(isMoving);

        }

        // Gets the current world position of the cat
        public Vector3 GetPosition()
        {
            return catView.transform.position;
        }

        // Gets the current grid position of the cat
        public Vector2Int GetGridPosition()
        {
            return position;
        }

        // Sets the cat's grid position and updates its orientation based on movement direction
        public void SetGridPosition(Vector2Int positionToSet)
        {

            Vector3 localPosition = catView.transform.localScale;
            localPosition.x *= (position.x < positionToSet.x) ? -1 : 1;
            catView.transform.localScale = localPosition;
            position = positionToSet ;
        }

        // Updates the cat's position towards the target position if it is moving
        public void UpdateCatMoving()
        {
            if (isMoving)
            {
                catView.transform.position = Vector3.Lerp(catView.transform.position, targetPosition, Time.deltaTime * moveSpeed);
                if (Vector3.Distance(catView.transform.position, targetPosition) < 0.01f)
                {
                    catView.transform.position = targetPosition;
                    isMoving = false;
                    catView.isMoving = isMoving;
                    catView.SetMoveAnimation(false);
                }
            }
        }
    }
}
