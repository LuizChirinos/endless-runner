using UnityEngine;
using UnityEngine.InputSystem;

namespace Triplano.Inputs
{
    public abstract class InputStrategy : ScriptableObject
    {
        protected PlayerInputActions playerInputActions;
        protected InputMovement inputMovement;

        public abstract void SubscribeToBindings(InputMovement inputMovement);
        public abstract void UnsubscribeToBindings(InputMovement inputMovement);

        public void EnableInput()
        {
            playerInputActions.Enable();
        }
        public void DisableInput()
        {
            playerInputActions.Disable();
        }
    }
}
