using System;
using Triplano.Inputs;
using UnityEngine;

namespace Triplano
{
    public class VerticalMovement : MonoBehaviour
    {
        public delegate void JumpEventHandler();

        public JumpEventHandler OnStartJumping;
        public JumpEventHandler OnStopJumping;

        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask floorMask;
        [SerializeField] private float toleranceToFloor = 0.3f;
        [SerializeField] private float rayDistance = 10f;
        [SerializeField] private float gravityMofidier = -10f;
        [SerializeField] private float maxJumpSpeed = 10f;
        [SerializeField] private float maxFallingSpeed = 10f;
        [SerializeField] private float playerHeight = 2.5f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius = 1f;
        [SerializeField] private AnimationCurve jumpTimeCurve;

        private bool canJump = true;
        private float floorHeight;
        private float jumpTime;
        private bool isJumping;
        private bool nearFloor;
        private float verticalSpeed;
        private InputMovement inputMovement;

        public float Gravity { get => gravityMofidier * jumpTimeCurve.Evaluate(jumpTime); }
        public bool NearFloor { get => nearFloor; }
        public Vector3 Origin { get => transform.position + offset; }
        public bool CanJump { get => canJump; set => canJump = value; }

        private void OnEnable()
        {
            inputMovement = GetComponent<InputMovement>();
            nearFloor = false;
            verticalSpeed = 0f;
            UpdateFloorHeight();

            inputMovement.OnMove += OnJump;
        }

        private void OnDisable()
        {
            inputMovement.OnMove -= OnJump;
        }

        private void FixedUpdate()
        {
            GravityCalculation();
        }

        private void GravityCalculation()
        {
            Collider[] collider = Physics.OverlapSphere(Origin, radius, floorMask);

            if (collider.Length > 0)
            {
                nearFloor = true;
                verticalSpeed -= Gravity;
                if (isJumping && jumpTime > 0.3f)
                {
                    jumpTime = 0f;
                    isJumping = false;
                    verticalSpeed -= verticalSpeed;
                    UpdateFloorHeight();
                    transform.localPosition = new Vector3(transform.localPosition.x, floorHeight, transform.localPosition.z);

                    OnStopJumping?.Invoke();
                }
            }
            else
                OnAir();

            if (isJumping)
            {
                jumpTime += Time.deltaTime;
            }

            verticalSpeed += Gravity;
            verticalSpeed = Mathf.Clamp(verticalSpeed, -maxFallingSpeed, maxJumpSpeed);
            transform.localPosition += new Vector3(0f, verticalSpeed, 0f) * Time.deltaTime;
        }

        private void OnAir()
        {
            Debug.Log("On air");
            nearFloor = false;
        }
        private void UpdateFloorHeight()
        {
            floorHeight = transform.localPosition.y;
        }

        private void OnJump(Vector2 delta)
        {
            if (isJumping && canJump)
                return;
            if (delta.y <= 0f)
                return;

            Jump();
        }

        public void AddVerticelSpeed(float speed)
        {
            verticalSpeed += speed;
        }

        [ContextMenu("Jump")]
        public void Jump()
        {
            jumpTime = 0f;
            isJumping = true;
            AddVerticelSpeed(jumpForce);

            OnStartJumping?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + offset, radius);
            //Debug.DrawLine(transform.position + offset, transform.position + offset - transform.up * toleranceToFloor, Color.red);
        }
    }
}
