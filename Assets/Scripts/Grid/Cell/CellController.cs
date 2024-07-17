

using System;
using TrapTheCat.Cat;
using TrapTheCat.Events;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace TrapTheCat.Grid.Cell
{
    public class CellController
    {
        private CellView cellView; // Reference to the cell view
        private Vector2Int gridPosition; // Position of the cell in the grid
        private EventService eventService; // Reference to the event service

        public bool IsBlocked { get; set; } // Indicates if the cell is blocked
        public bool IsCorner { get; set; } // Indicates if the cell is a corner

        // Constructor to initialize the CellController with a cell view prefab, parent container, and event service
        public CellController(CellView cellViewPrefab, Transform parentContainer, EventService eventService)
        {
            this.eventService = eventService;
            cellView = UnityEngine.Object.Instantiate(cellViewPrefab, parentContainer);
            cellView.SetController(this);
        }

        // Blocks the cell and changes its color, then invokes the block cell event
        public void Block()
        {
            if ((!IsBlocked&&!IsCorner))
            {
                IsBlocked = true;
                cellView.SetColor(new Color32(0xAA, 0xAA, 0xAF, 0xFF));
                eventService.OnBlockCell.InvokeEvent(gridPosition);
            }
        }

        // Sets the cell as a corner and changes its color, then invokes the block cell event
        public void Corner()
        {
            if (!IsBlocked)
            {
                IsCorner=true;
                cellView.SetColor(new Color32(255, 255, 255, 0));
                eventService.OnBlockCell.InvokeEvent(gridPosition);
            }
        }

        // Sets the grid position of the cell
        public void SetGridPosition(Vector2Int positionToSet) => gridPosition = positionToSet;

        // Gets the grid position of the cell
        public Vector2Int GetGridPosition() => gridPosition;

        // Sets the world position of the cell
        public void SetPosition(Vector3 spawnPosition)
        {
            cellView.transform.position = spawnPosition;
        }

        // Gets the world position of the cell
        public Vector3 GetPosition()
        {
            return cellView.transform.position;
        }
    }
}
