using System.Collections;
using Triplano.Inputs;
using Triplano.Lanes;
using UnityEngine;
using UnityEngine.Events;

namespace Triplano.Movement
{
    [RequireComponent(typeof(InputMovement))]
    public class JumpMovement : MonoBehaviour, IMove
    {
        public delegate void JumpEventHandler();
        public JumpEventHandler OnStartJumping;
        public JumpEventHandler OnStopJumping;

        public UnityEvent OnStartJumpingUnityEvent;
        public UnityEvent OnStopJumpingUnityEvent;

        [SerializeField] private int movementLock;
        private Vector3 currentSpeed;
        private InputMovement inputMovement;
        private bool isJumping = false;
        private float floorHeight = 0f;

        public int MovementLock => movementLock;

        public bool CanMove => movementLock <= 0;

        public Vector3 CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

        [SerializeField] private LaneMovementData laneMovementData;

        private void Start()
        {
            isJumping = false;
            inputMovement = GetComponent<InputMovement>();
        }

        private void Update()
        {
            if (!CanMove)
                return;

            if (inputMovement.Movement.y > 0f)
                Move(Vector3.up);
        }
        public void Move(Vector3 direction)
        {
            if (direction != Vector3.up || isJumping)
                return;

            StartCoroutine(JumpCoroutine(laneMovementData.DurationOfJump));
        }

        public void LockMovement()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockMovement()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator JumpCoroutine(float duration)
        {
            isJumping = true;

            float intialTime = Time.time;
            float elapsedTime = Time.time - intialTime;

            OnStartJumping?.Invoke();
            OnStartJumpingUnityEvent?.Invoke();

            Vector3 jumpPosition;

            while (elapsedTime / duration <= 1f)
            {
                elapsedTime = Time.time - intialTime;
                jumpPosition = new Vector3(transform.localPosition.x,
                                                   laneMovementData.JumpCurve.Evaluate(elapsedTime / duration) * laneMovementData.JumpHeight,
                                                   transform.localPosition.z);
                transform.localPosition = jumpPosition;
                CurrentSpeed = jumpPosition - transform.localPosition;
                yield return null;
            }

            jumpPosition = new Vector3(transform.localPosition.x,
                                   floorHeight,
                                   transform.localPosition.z);

            transform.localPosition = jumpPosition;

            isJumping = false;

            OnStopJumping?.Invoke();
            OnStopJumpingUnityEvent?.Invoke();

        }
    }
}
