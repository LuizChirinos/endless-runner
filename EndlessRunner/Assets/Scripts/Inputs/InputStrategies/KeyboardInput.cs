using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triplano.Inputs
{
    [CreateAssetMenu(fileName = nameof(KeyboardInput), menuName = "Inputs/Keyboard")]
    public class KeyboardInput : InputStrategy
    {
        public override void SubscribeToBindings(InputMovement inputMovement)
        {
            this.inputMovement = inputMovement;
            playerInputActions = new PlayerInputActions();

            playerInputActions.Player.Movement.performed += context => Move(context);
            playerInputActions.Player.Movement.canceled += context => Move(context);
        }

        private void Move(InputAction.CallbackContext context)
        {
            inputMovement.Movement = context.ReadValue<Vector2>();
        }

        public override void UnsubscribeToBindings(InputMovement inputMovement)
        {
            playerInputActions.Player.Movement.performed -= context => Move(context);
            playerInputActions.Player.Movement.canceled -= context => Move(context);
        }
    }
}
