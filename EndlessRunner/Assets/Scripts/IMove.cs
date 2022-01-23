using UnityEngine;

namespace Triplano.Movement
{
    public interface IMove
    {
        public int MovementLock { get; }
        public bool CanMove { get; }
        public Vector3 CurrentSpeed { get; set; }

        public void Move(Vector3 direction);
        public void LockMovement();
        public void UnlockMovement();
    }
}