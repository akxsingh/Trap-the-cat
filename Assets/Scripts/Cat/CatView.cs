using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TrapTheCat.Cat
{
    public class CatView : MonoBehaviour
    {
        public CatController controller; // Reference to the CatController
        [SerializeField] Animator catAnimator; // Animator component for the cat
        [SerializeField] SpriteRenderer catSpriteRender; // SpriteRenderer component for the cat
        public bool isMoving; // Indicates if the cat is currently moving
        public bool isNextMoveCurrentPos; // Indicates if the next move is the current position



        // Sets the controller for the CatView
        public void SetController(CatController controller)
        {
            this.controller = controller;
        }

        // Sets the animation state for the cat's movement
        public SpriteRenderer GetSpriteRender()
        {
           return catSpriteRender;
        }

        // Gets the current animation state for the cat's movement
        public void SetMoveAnimation(bool isMoving)
        {
            catAnimator.SetBool("IsWalking", isMoving);
        }

        // Gets the current animation state for the cat's movement
        public bool GetMoveAnimation()
        {
            return catAnimator.GetBool("IsWalking");
        }

        // Unity's Update method, called once per frame
        void Update()
        {
            controller.UpdateCatMoving();
        }
    }
}