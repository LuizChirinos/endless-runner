using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triplano.Inputs
{
    [CreateAssetMenu(fileName = nameof(SwipeInput), menuName = "Inputs/SwipeInput")]
    public class SwipeInput : InputStrategy
    {
        public Action<Vector2> OnSwipe;

        [SerializeField] private bool hasMaxSwipeTime = false;
        [SerializeField] private bool hasInputTolerance = false;
        [SerializeField] private float maxSwipeTime = 0.2f;
        [SerializeField] private float verticalTolerance = 200f;
        [SerializeField] private float horizontalTolerance = 50f;

        private float swipeStartTime;
        private Vector2 startPosition;
        private Vector2 endPosition;
        private Vector2 delta;

        private bool pressed;

        public override void SubscribeToBindings(InputMovement inputMovement)
        {
            this.inputMovement = inputMovement;
            playerInputActions = new PlayerInputActions();

            playerInputActions.Player.TouchContact.started += context => StartTouch(context);
            playerInputActions.Player.TouchContact.canceled += context => EndTouch(context);
        }

        public override void UnsubscribeToBindings(InputMovement inputMovement)
        {
            playerInputActions.Player.TouchContact.started -= context => StartTouch(context);
            playerInputActions.Player.TouchContact.canceled -= context => EndTouch(context);
        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            startPosition = playerInputActions.Player.TouchPosition.ReadValue<Vector2>();
            swipeStartTime = (float)context.startTime;
        }

        private void EndTouch(InputAction.CallbackContext context)
        {
            endPosition = playerInputActions.Player.TouchPosition.ReadValue<Vector2>();
            float swipeEndTime = swipeStartTime - (float)context.time;

            delta = endPosition - startPosition;

            if (hasMaxSwipeTime && swipeEndTime > maxSwipeTime)
                return;

            if(hasInputTolerance)
                delta = SetInputTolerance(delta);
            inputMovement.Move(delta);
        }

        private Vector2 SetInputTolerance(Vector2 delta)
        {
            if (Mathf.Abs(delta.x) < horizontalTolerance)
                delta.x = 0f;
            if (Mathf.Abs(delta.y) < verticalTolerance)
                delta.y = 0f;
            return delta;
        }
    }
}
