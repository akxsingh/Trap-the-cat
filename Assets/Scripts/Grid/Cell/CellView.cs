using System.Collections;
using System.Collections.Generic;
using TrapTheCat.Cat;
using UnityEngine;

namespace TrapTheCat.Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        private CellController controller; // Reference to the CellController
        [SerializeField] private SpriteRenderer spriteRenderer; // SpriteRenderer component for the cell



        // Sets the controller for the CellView
        public void SetController(CellController controller)
        {
            this.controller = controller;
        }

        // Sets the color of the cell
        public void SetColor(Color colortToSet) => spriteRenderer.color = colortToSet;

        // Unity's method called when the mouse is pressed over the Collider
        void OnMouseDown()
        {
            CatView catView = GameObject.FindGameObjectWithTag("Player").GetComponent<CatView>();
            CatController catController = catView.controller;
            if (!catView.isMoving)
            {
                controller.Block();
            }
        }
    }
}