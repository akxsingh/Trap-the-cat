using System.Collections.Generic;
using System.Linq;
using TrapTheCat.Events;
using TrapTheCat.Grid;
using UnityEngine;

namespace TrapTheCat.Cat
{
    public class CatService 
    {

        private EventService eventService; // Service for handling events
        private GridService gridService; // Service for handling grid operations
        public CatController catController; // Controller for the cat
        private CatView catViewPrefab; // Prefab for the cat view
        private Vector3 catInitPosition; // Initial position of the cat
        private UIService uiService; // Service for handling UI updates

        // Constructor to initialize the CatService with a CatView prefab
        public CatService(CatView catViewPrefab)
        {
            this.catViewPrefab = catViewPrefab;
        }

        // Initializes the CatService with event, grid, and UI services
        public void Init(EventService eventService, GridService gridService, UIService uiService)
        {
            this.eventService = eventService;
            this.gridService = gridService;
            this.uiService = uiService;
            SubscriveEvents();
            SpawnCat();
        }

        // Subscribes to necessary events
        private void SubscriveEvents()
        {
            eventService.OnBlockCell.AddListener(OnBlockCell);
        }


        // Spawns the cat at the center of the grid
        private void SpawnCat()
        {
            catController = new CatController(catViewPrefab, gridService.GetCenterGridPosition());
            catInitPosition = catController.GetPosition();
            Vector3 catPosition = gridService.GetCenterCellPosition() + catInitPosition;
            catController.SetPosition(catPosition);
        }

        // Event handler for when a cell is blocked
        private void OnBlockCell(Vector2Int position)
        {
          
            Move();
            CheckGameState();
        }

        // Moves the cat to the next best position
        public void Move()
        {
            Vector2Int nextMove = GetBestMove();
           
            Vector2Int position = catController.GetGridPosition();
            if (nextMove != position)
            {
                catController.SetGridPosition(nextMove);
                Vector3 catPosition = gridService.GetCellPosition(nextMove) + catInitPosition;
                catController.SetPosition(catPosition);
            }

            
        }

        // Determines the best move for the cat
        private Vector2Int GetBestMove()
        {
            List<Vector2Int> possibleMoves = GetPossibleMoves(catController.GetGridPosition());
            if (possibleMoves.Count == 0) return catController.GetGridPosition();

            // Find the shortest path to top or bottom edge
            Vector2Int shortestPathMove = FindShortestPathToTopOrBottom();
            if (shortestPathMove != Vector2Int.zero)
            {
                return shortestPathMove;
            }

            // If no path to top or bottom, fallback to the original strategy
            return possibleMoves.OrderBy(move => gridService.GetDistanceToGridEdge(move)).First();
        }

        // Finds the shortest path to the top or bottom edge of the grid
        private Vector2Int FindShortestPathToTopOrBottom()
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Dictionary<Vector2Int, Vector2Int> parentMap = new Dictionary<Vector2Int, Vector2Int>();
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

            Vector2Int start = catController.GetGridPosition();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();

                if (current.y == 0 || current.y == gridService.GetGridSize().y - 1)
                {
                    // Found a path to top or bottom edge
                    return ReconstructPath(parentMap, start, current);
                }

                foreach (Vector2Int neighbor in GetPossibleMoves(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                    }
                }
            }

            return Vector2Int.zero; // No path found
        }

        // Reconstructs the path from the start to the end position
        private Vector2Int ReconstructPath(Dictionary<Vector2Int, Vector2Int> parentMap, Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            Vector2Int current = end;

            while (current != start)
            {
                path.Add(current);
                current = parentMap[current];
            }

            path.Reverse();
            return path.Count > 0 ? path[0] : start;
        }

        // Gets a list of possible moves for the cat from a given position
        private List<Vector2Int> GetPossibleMoves(Vector2Int position)
        {
            List<Vector2Int> moves = new List<Vector2Int>();
            Vector2Int[] directions;

            if (position.y % 2 == 0)
            {
                // Even rows
                directions = new Vector2Int[]
                {
                    new Vector2Int(0, 1), new Vector2Int(1, 1),
                    new Vector2Int(1, 0), new Vector2Int(1, -1),
                    new Vector2Int(0, -1), new Vector2Int(-1, 0)
                };
            }
            else
            {
                // Odd rows
                directions = new Vector2Int[]
                {
                    new Vector2Int(-1, 1), new Vector2Int(0, 1),
                    new Vector2Int(1, 0), new Vector2Int(0, -1),
                    new Vector2Int(-1, -1), new Vector2Int(-1, 0)
                };
            }

            foreach (Vector2Int dir in directions)
            {
                Vector2Int newPos = position + dir;
                if (!gridService.IsCellBlocked(newPos.x, newPos.y))
                {
                    moves.Add(newPos);
                }
            }
            return moves;
        }


        // Checks if the cat has escaped the grid
        public bool HasEscaped()
        {
            return  gridService.HasEscaped(catController.GetGridPosition());
        }

        // Checks if the cat is trapped with no possible moves
        public bool IsTrapped()
        {
            return GetPossibleMoves(catController.GetGridPosition()).Count == 0;
        }

        // Checks the game state and updates the UI and events accordingly
        public void CheckGameState()
        {
            if (HasEscaped())
            {
                uiService.SetGameText("Meow....Ahh!! Cat Escaped! You Lose!");
                eventService.OnGameOver.InvokeEvent(true);
            }
            else if (IsTrapped())
            {
                uiService.SetGameText("Cat Trapped! You Win!");
                eventService.OnGameOver.InvokeEvent(true);
            }
        }
        // Destructor to remove event listeners
        ~CatService()
        {
            eventService.OnBlockCell.RemoveListener(OnBlockCell);
        }
    }
}
