
using TrapTheCat.Cat; // Importing CatService and CatView
using TrapTheCat.Events; // Importing EventService
using TrapTheCat.Grid; // Importing GridService and GridSO
using UnityEngine;


namespace TrapTheCat
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService; // Event service instance
        private GridService gridService; // Grid service instance
        private CatService catService; // Cat service instance

        [SerializeField] private GridSO gridSO; // Reference to GridSO asset in Unity editor
        [SerializeField] private CatView catViewPrefab; // Prefab for visual representation of the cat

        [SerializeField] private UIService uiService; // Reference to UIService for UI management

        public UIService UIService => uiService; // Property to access UIService instance

        private void Start()
        {
            InitializeServices(); // Initialize event, grid, and cat services
            InjectDependencies(); // Initialize dependencies between services
            SetCameraPosition(); // Adjust camera position based on grid size
        }



        // Initializes event, grid, and cat services
        private void InitializeServices()
        {
            eventService = new EventService(); // Instantiate EventService
            gridService = new GridService(gridSO); // Instantiate GridService with GridSO data
            catService = new CatService(catViewPrefab); // Instantiate CatService with cat view prefab
        }


        // Injects dependencies between services
        private void InjectDependencies()
        {
            gridService.Init(eventService); // Initialize GridService with EventService dependency
            catService.Init(eventService, gridService, uiService); // Initialize CatService with EventService, GridService, and UIService dependencies
            UIService.Init(eventService); // Initialize UIService with EventService dependency
        }


        // Adjusts the camera position based on the grid dimensions
        private void SetCameraPosition()
        {
            // Calculate total width and height of the grid
            float totalWidth = gridSO.GridColumn * (gridSO.CellSize + gridSO.CellSpacing);
            float totalHeight = gridSO.GridRow * (gridSO.CellSize + gridSO.CellSpacing);

            // Determine the smaller value between total width and height
            float smallerValue = Mathf.Max(totalWidth, totalHeight);

            // Adjust the orthographic size of the main camera
            Camera.main.orthographicSize = smallerValue * 0.6f;
        }
    }

}
