using System.Collections;
using Triplano.Inputs;
using Triplano.Movement;
using UnityEngine;

namespace Triplano.Lanes
{
    [RequireComponent(typeof(InputMovement))]
    public class LaneMovement : MonoBehaviour, IMove
    {
        public delegate void ChangeLaneHandler(Vector3 direction);
        public ChangeLaneHandler OnChangingLane;

        [SerializeField] private LaneData laneData;
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float durationOfMovement;

        private InputMovement inputMovement;
        private bool isChangingLanes = false;
        private int movementLock = 0;
        private Vector3 currentSpeed;

        public bool IsChangingLanes { get => isChangingLanes; }
        public int MovementLock { get => movementLock; }
        public bool CanMove { get => MovementLock <= 0; }
        public Vector3 CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

        private void Start()
        {
            inputMovement = GetComponent<InputMovement>();
        }

        private void Update()
        {
            if (!CanMove)
                return;

            Move(inputMovement.Movement);
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

        public void Move(Vector3 direction)
        {
            if (direction.x == 0f || isChangingLanes)
                return;

            StartCoroutine(ChangeLaneCoroutine(direction, durationOfMovement));
        }

        private IEnumerator ChangeLaneCoroutine(Vector3 direction, float duration)
        {
            isChangingLanes = true;

            float intialTime = Time.time;
            float elapsedTime = Time.time - intialTime;

            Vector3 targetLane = transform.localPosition + Vector3.right * Mathf.Sign(direction.x) * laneData.Spacing;

            if (Mathf.Sign(direction.x) > 0)
                OnChangingLane?.Invoke(Vector3.right);
            else if (Mathf.Sign(direction.x) < 0)
                OnChangingLane?.Invoke(Vector3.left);

            while (elapsedTime/duration < 0.8f)
            {
                elapsedTime = Time.time - intialTime;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLane, movementCurve.Evaluate(elapsedTime / duration));
                CurrentSpeed = targetLane - transform.localPosition;
                yield return null;
            }

            transform.localPosition = targetLane;

            isChangingLanes = false;
        }
    }
}
