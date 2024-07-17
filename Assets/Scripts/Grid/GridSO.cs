using TrapTheCat.Grid.Cell;
using UnityEngine;

namespace TrapTheCat.Grid
{
    // Attribute to create a menu item for creating instances of GridSO

    [CreateAssetMenu(fileName = "GridScriptableObject", menuName = "ScriptableObjects/GridScriptableObject")]
    public class GridSO : ScriptableObject
    {
        // Public variables defining properties of the grid

        public int GridRow; // Number of rows in the grid
        public int GridColumn; // Number of columns in the grid
        public float RowOffset; // Offset applied to even rows for zigzag pattern
        public float CellSize; // Size of each cell in the grid
        public float CellSpacing; // Spacing between cells in the grid
        public bool IsZigZag; // Flag indicating if grid layout follows a zigzag pattern
        public CellView CellViewPrefab; // Prefab used for visual representation of cells
        public int BlockedCellCount; // Number of cells initially blocked in the grid

    }
}
