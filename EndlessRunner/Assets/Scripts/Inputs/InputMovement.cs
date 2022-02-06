using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Triplano.Inputs
{
    public class InputMovement : MonoBehaviour
    {
        public Action<Vector2> OnMove;

        [SerializeField] protected InputStrategy inputStrategy;

        public Vector3 Movement = Vector3.zero;

        protected virtual void OnEnable()
        {
            Initialize();
        }

        private void UnsubscribeToBindings()
        {
            inputStrategy.UnsubscribeToBindings(this);
        }

        private void Initialize()
        {
            inputStrategy.SubscribeToBindings(this);
            Enable();
        }

        protected virtual void OnDisable()
        {
            Disable();
            UnsubscribeToBindings();
        }

        public void Enable() => inputStrategy.EnableInput();
        public void Disable() => inputStrategy.DisableInput();

        public void Move(Vector2 delta)
        {
            OnMove?.Invoke(delta);
        }
    }

}