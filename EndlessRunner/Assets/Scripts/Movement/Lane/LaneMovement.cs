using System.Collections;
using Triplano.Inputs;
using Triplano.Movement;
using UnityEngine;

namespace Triplano.Lanes
{
    public class LaneMovement : MonoBehaviour, IMove
    {
        public delegate void MovementEventHandler();
        public MovementEventHandler OnStartMoving;
        public MovementEventHandler OnStopMoving;

        public delegate void ChangeLaneHandler(Vector3 direction);
        public ChangeLaneHandler OnChangingLane;
        public ChangeLaneHandler OnTriedChangingLane;

        [SerializeField] private LaneMovementData laneMovementData;
        [SerializeField] private int indexOfCurrentLane;

        private InputMovement inputMovement;
        private bool isChangingLanes = false;
        private int movementLock = 0;
        private Vector3 currentSpeed;
        private bool forceStopMoving;

        public bool IsChangingLanes { get => isChangingLanes; }
        public int MovementLock { get => movementLock; }
        public bool CanMove { get => MovementLock <= 0; }
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
        public int CurrentLane { get => indexOfCurrentLane; }

        private void OnEnable()
        {
            inputMovement = GetComponent<InputMovement>();
            CurrentSpeed = Vector3.zero;
            forceStopMoving = false;

            inputMovement.OnMove += OnMove;
        }

        private void OnDisable()
        {
            inputMovement.OnMove -= OnMove;
        }

        public void LockMovement()
        {
            movementLock++;
        }

        public void UnlockMovement()
        {
            movementLock--;
        }

        public void MoveLeft()
        {
            Move(Vector3.left);
        }

        public void MoveRight()
        {
            Move(Vector3.right);
        }

        private void OnMove(Vector2 delta)
        {
            Move(delta);
        }

        public void Move(Vector3 direction)
        {
            if (!CanMove)
                return;

            if (direction.x == 0f || isChangingLanes)
                return;

            StartCoroutine(ChangeLaneCoroutine(direction, laneMovementData.DurationOfMovement));
        }

        public void StopMove()
        {
            forceStopMoving = true;
            LockMovement();
        }

        private IEnumerator ChangeLaneCoroutine(Vector3 direction, float duration)
        {
            if (IsMovingOnLaneBoundary(direction))
                yield break;

            isChangingLanes = true;

            if (Mathf.Sign(direction.x) > 0)
            {
                OnChangingLane?.Invoke(Vector3.right);
                indexOfCurrentLane++;
            }
            else if (Mathf.Sign(direction.x) < 0)
            {
                OnChangingLane?.Invoke(Vector3.left);
                indexOfCurrentLane--;
            }

            float intialTime = Time.time;
            float elapsedTime = Time.time - intialTime;

            Vector3 targetLane = transform.localPosition + Vector3.right * Mathf.Sign(direction.x) * laneMovementData.Spacing;

            while (elapsedTime / duration < 0.8f && !forceStopMoving)
            {
                elapsedTime = Time.time - intialTime;
                targetLane.y = transform.localPosition.y;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLane, laneMovementData.MovementCurve.Evaluate(elapsedTime / duration));
                CurrentSpeed = targetLane - transform.localPosition;
                yield return null;
            }

            isChangingLanes = false;

            if (forceStopMoving)
                yield break;

            targetLane.y = transform.localPosition.y;
            transform.localPosition = targetLane;

        }

        private bool IsMovingOnLaneBoundary(Vector3 direction)
        {
            if (Mathf.Sign(direction.x) > 0 && laneMovementData.ReturnLaneBoundary(indexOfCurrentLane) > 0)
            {
                OnTriedChangingLane?.Invoke(Vector3.right);
                return true;
            }
            else if (Mathf.Sign(direction.x) < 0 && laneMovementData.ReturnLaneBoundary(indexOfCurrentLane) < 0)
            {
                OnTriedChangingLane?.Invoke(Vector3.left);
                return true;
            }
            return false;
        }
    }
}
