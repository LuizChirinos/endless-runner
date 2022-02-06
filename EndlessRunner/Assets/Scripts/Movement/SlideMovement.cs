using System;
using System.Collections;
using System.Collections.Generic;
using Triplano.Inputs;
using Triplano.Lanes;
using UnityEngine;
using UnityEngine.Events;

namespace Triplano.Movement
{
    public class SlideMovement : MonoBehaviour, IMove
    {
        public delegate void SlideEventHandler();
        public SlideEventHandler OnStartSliding;
        public SlideEventHandler OnStopSliding;

        public UnityEvent OnStartSlidingUnityEvent;
        public UnityEvent OnStopSlidingUnityEvent;

        [SerializeField] private LaneMovementData laneMovementData;
        [SerializeField] private int movementLock;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Collider sliderCollider;
        private InputMovement inputMovemet;
        private Vector3 currentSpeed;
        private bool isSliding;

        public int MovementLock => movementLock;

        public bool CanMove => movementLock <= 0;

        public Vector3 CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
        public bool IsSliding { get => isSliding; }

        private void OnEnable()
        {
            isSliding = false;
            inputMovemet = GetComponent<InputMovement>();
            ToggleSliderCollider();
            inputMovemet.OnMove += OnMove;
        }

        private void OnDisable()
        {
            inputMovemet.OnMove -= OnMove;
        }

        private void OnMove(Vector2 delta)
        {
            Move(delta);
        }

        public void Move(Vector3 direction)
        {
            if (!CanMove)
                return;

            if (direction.y >= 0f)
                return;

            if (isSliding)
                return;

            StartCoroutine(SlideCoroutine(laneMovementData.DurationOfSlide));
        }

        public void LockMovement()
        {
            movementLock++;
        }

        public void UnlockMovement()
        {
            movementLock--;
        }

        private IEnumerator SlideCoroutine(float duration)
        {
            isSliding = true;

            float intialTime = Time.time;
            float elapsedTime = Time.time - intialTime;

            ToggleSliderCollider();

            OnStartSliding?.Invoke();
            OnStartSlidingUnityEvent?.Invoke();


            while (elapsedTime / duration < 0.8f)
            {
                elapsedTime = Time.time - intialTime;
                yield return null;
            }

            isSliding = false;

            ToggleSliderCollider();

            OnStopSliding?.Invoke();
            OnStopSlidingUnityEvent?.Invoke();

        }

        private void ToggleSliderCollider()
        {
            playerCollider.enabled = !isSliding;
            sliderCollider.enabled = isSliding;
        }
    }
}
