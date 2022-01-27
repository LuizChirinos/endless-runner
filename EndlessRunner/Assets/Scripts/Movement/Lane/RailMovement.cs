using Triplano.Movement;
using UnityEngine;

namespace Triplano.Lanes
{
    public class RailMovement : MonoBehaviour, IMove
    {
        public delegate void MovementEventHandler();
        public MovementEventHandler OnStartMoving;
        public MovementEventHandler OnStopMoving;

        [SerializeField] private MovementData movementData;
        [SerializeField] private int movementLock;
        private Vector3 currentSpeed;
        public int MovementLock => movementLock;

        public bool CanMove => movementLock <= 0;

        public Vector3 CurrentSpeed
        { 
            get => currentSpeed;
            set
            {
                if (currentSpeed.magnitude == 0f && value.magnitude != 0f)
                    OnStartMoving?.Invoke();

                if (value.magnitude == 0f)
                    OnStopMoving?.Invoke();

                currentSpeed = value;
            }
        }

        public void LockMovement()
        {
            movementLock++;
        }

        public void Move(Vector3 direction)
        {
            CurrentSpeed = direction * Time.deltaTime * movementData.Speed;
            transform.localPosition += CurrentSpeed;
        }

        public void UnlockMovement()
        {
            movementLock--;
        }

        private void Start()
        {
            CurrentSpeed = Vector3.zero;
        }
        private void FixedUpdate()
        {
            if (!CanMove)
                return;

            Move(Vector3.forward);
        }
    }

}