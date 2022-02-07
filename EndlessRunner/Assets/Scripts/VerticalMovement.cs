using System;
using Triplano.Inputs;
using Triplano.Movement;
using UnityEngine;

namespace Triplano
{
    public class VerticalMovement : MonoBehaviour, IMove
    {
        public delegate void JumpEventHandler();

        public JumpEventHandler OnStartJumping;
        public JumpEventHandler OnStopJumping;

        [Header("Collider casting")]
        [SerializeField] private LayerMask floorMask;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float toleranceToFloor = 0.3f;
        [SerializeField] private float rayDistance = 10f;
        [SerializeField] private float radius = 1f;
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float maxJumpSpeed = 10f;
        [SerializeField] private float playerHeight = 2.5f;
        [SerializeField] private AnimationCurve jumpTimeCurve;
        [Header("Fall")]
        [SerializeField] private float maxFallingSpeed = 10f;
        [SerializeField] private float gravityMofidier = -10f;
        [SerializeField] private float fallInputModifier = 2f;
        [Header("Slope")]
        [SerializeField] private float raycastSlopeAngle = 45f;
        [SerializeField] private float slopeDetectionDistance = 2f;

        private int movementLock;
        private bool canJump = true;
        private float floorHeight;
        private float jumpTime;
        private bool isJumping;
        private bool nearFloor;
        private float verticalSpeed;
        private RaycastHit hit;
        private InputMovement inputMovement;

        public float Gravity
        {
            get
            {
                float gravityScale = 100f;
                if (isJumping)
                    gravityScale = gravityMofidier * jumpTimeCurve.Evaluate(jumpTime);
                else
                    gravityScale = gravityMofidier;
                return gravityScale;
            }
        }

        public bool NearFloor { get => nearFloor; }

        public Vector3 Origin { get => transform.position + offset; }

        public Vector3 SlopeTargetOrigin { get => transform.position + transform.up; }

        public Vector3 SlopeTargetDirection { get => transform.forward - transform.up; }

        public bool CanJump { get => canJump; set => canJump = value; }

        public int MovementLock => movementLock;

        public bool CanMove => movementLock <= 0;

        public Vector3 CurrentSpeed { get => new Vector3(0f, verticalSpeed, 0f); set => verticalSpeed = value.y; }

        private void OnEnable()
        {
            inputMovement = GetComponent<InputMovement>();
            nearFloor = false;
            verticalSpeed = 0f;
            UpdateFloorHeight();

            inputMovement.OnMove += OnJump;
            inputMovement.OnMove += OnFall;
        }

        private void OnDisable()
        {
            inputMovement.OnMove -= OnJump;
            inputMovement.OnMove = OnFall;
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
                OnGround();
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

        private void OnGround()
        {
            nearFloor = true;

            if (!isJumping)
                verticalSpeed = 0f;
            //verticalSpeed -= Gravity;
            if (isJumping && jumpTime > 0.3f)
                ReturningFromJump();

            DetectecSlope();
        }

        private void DetectecSlope()
        {
            Ray ray = new Ray(SlopeTargetOrigin, SlopeTargetDirection);
            if (Physics.Raycast(ray, out hit, 10f, floorMask))
            {
                if (hit.collider && !isJumping && hit.distance <= slopeDetectionDistance)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     hit.point.y,
                                                     transform.position.z);

                    Debug.Log(hit.collider.name);
                    UpdateFloorHeight();
                }
            }
        }

        private void ReturningFromJump()
        {
            jumpTime = 0f;
            isJumping = false;
            verticalSpeed -= verticalSpeed;
            UpdateFloorHeight();
            transform.localPosition = new Vector3(transform.localPosition.x, floorHeight, transform.localPosition.z);

            Ray ray = new Ray(transform.position + transform.up, -transform.up);
            if (Physics.Raycast(ray, out hit, 10f, floorMask))
            {
                if (hit.collider)
                {
                    transform.position = new Vector3(transform.position.x,
                                                          (hit.point).y,
                                                          transform.position.z);
                }
            }

            OnStopJumping?.Invoke();
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
            if (isJumping || !CanMove)
                return;
            if (delta.y <= 0f)
                return;

            Jump();
        }

        private void OnFall(Vector2 delta)
        {
            if (!isJumping || !CanMove)
                return;
            if (delta.y >= 0f)
                return;
            Fall();
        }

        public void SetVerticalSpeed(float speed)
        {
            verticalSpeed = speed;
        }

        [ContextMenu("Jump")]
        public void Jump()
        {
            jumpTime = 0f;
            isJumping = true;
            SetVerticalSpeed(jumpForce);

            OnStartJumping?.Invoke();
        }

        [ContextMenu("Fall")]
        public void Fall()
        {
            float verticalAbsolute = Mathf.Abs(verticalSpeed);
            verticalSpeed = -verticalAbsolute * fallInputModifier;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + offset, radius);
            Debug.DrawLine(SlopeTargetOrigin, SlopeTargetOrigin + SlopeTargetDirection, Color.red);
        }

        public void Move(Vector3 direction)
        {
            throw new NotImplementedException();
        }

        public void LockMovement()
        {
            movementLock++;
        }

        public void UnlockMovement()
        {
            movementLock--;
        }
    }
}
