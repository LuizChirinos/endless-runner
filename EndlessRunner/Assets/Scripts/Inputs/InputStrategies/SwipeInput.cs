using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triplano.Inputs
{
    [CreateAssetMenu(fileName = nameof(SwipeInput), menuName = "Inputs/SwipeInput")]
    public class SwipeInput : InputStrategy
    {
        [SerializeField] private float maxSwipeTime = 0.2f;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 delta;

        public override void SubscribeToBindings(InputMovement inputMovement)
        {
            this.inputMovement = inputMovement;
            playerInputActions = new PlayerInputActions();

            playerInputActions.Player.TouchContact.performed += context => StartInput(context);
            playerInputActions.Player.TouchContact.canceled += context => EndInput(context);
        }
        public override void UnsubscribeToBindings(InputMovement inputMovement)
        {
            playerInputActions.Player.TouchContact.performed -= context => StartInput(context);
            playerInputActions.Player.TouchContact.canceled -= context => EndInput(context);
        }

        private void StartInput(InputAction.CallbackContext context)
        {
            startPosition = context.ReadValue<Vector2>();
            startTime = (float)context.startTime;
        }

        private void EndInput(InputAction.CallbackContext context)
        {
            float swipeTime = startTime - (float)context.time;

            if (swipeTime > maxSwipeTime)
                return;

            Vector2 endPosition = context.ReadValue<Vector2>();
            delta = endPosition - startPosition;

            inputMovement.Movement = delta;

            startPosition = endPosition;
        }
    }
}
