using System;
using System.Collections;
using System.Collections.Generic;
using TrapTheCat.Events;
using UnityEngine;

namespace TrapTheCat.Grid
{
    public class GridService 
    {

        public GridController gridController; // Instance of GridController
        private EventService eventService; // Reference to the event service
        private GridSO gridSO; // Reference to the grid scriptable object

        // Constructor to initialize the GridService with grid data
        public GridService(GridSO gridData)
        {
            this.gridSO = gridData;
        }

        // Initializes the GridService with an event service and creates a GridController
        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            gridController = new GridController(gridSO, eventService);
        }

        public Vector3 GetCenterCellPosition()
        {
            return gridController.GetCenterCellPosition();
        }

        // Gets the world position of the center cell
        public Vector3 GetCellPosition(Vector2Int gridPosition)
        {
            return gridController.GetCellPosition(gridPosition);
        }

        // Checks if a cell at given coordinates is blocked
        public bool IsCellBlocked(int x, int y)
        {
            return gridController.IsCellBlocked(x, y);
        }

        // Gets the grid position of the center cell
        public Vector2Int GetCenterGridPosition()
        {
            return gridController.GetCenterGridPosition();
        }

        // Gets the distance from a given position to the edge of the grid
        public float GetDistanceToGridEdge(Vector2Int pos)
        {
            return gridController.GetDistanceToGridEdge(pos);
        }

        // Checks if the given position is at the edge of the grid
        public bool HasEscaped(Vector2Int position)
        {
           return gridController.HasEscaped(position);
            
        }

        // Gets the size of the grid
        public Vector2Int GetGridSize() {
            return new Vector2Int(gridSO.GridRow, gridSO.GridColumn);
        }
    }
}


