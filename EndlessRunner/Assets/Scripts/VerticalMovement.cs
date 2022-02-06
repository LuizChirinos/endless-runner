using Triplano.Inputs;
using UnityEngine;

namespace Triplano
{
    public class VerticalMovement : MonoBehaviour
    {
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
        [SerializeField] private bool jump;
        [SerializeField] private AnimationCurve jumpTimeCurve;

        private float floorHeight;
        private float jumpTime;
        private bool jumping;
        private bool nearFloor;
        private float verticalSpeed;
        private InputMovement inputMovement;

        public float Gravity { get => gravityMofidier * jumpTimeCurve.Evaluate(jumpTime); }
        public bool NearFloor { get => nearFloor; }
        public Vector3 Origin { get => transform.position + offset; }

        private void Start()
        {
            nearFloor = false;
            verticalSpeed = 0f;
            UpdateFloorHeight();
        }

        private void OnValidate()
        {
            if (jump)
            {
                Jump();
                jump = false;
            }
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
                if (jumping && jumpTime > 0.3f)
                {
                    jumpTime = 0f;
                    jumping = false;
                    verticalSpeed -= verticalSpeed;
                    UpdateFloorHeight();
                    transform.localPosition = new Vector3(transform.localPosition.x, floorHeight, transform.localPosition.z);
                }
            }
            else
                OnAir();

            if (jumping)
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

        public void SetVerticalSpeed(float speed)
        {
            verticalSpeed = speed;
            jumpTime = 0f;
            jumping = true;
        }

        [ContextMenu("Jump")]
        public void Jump()
        {
            SetVerticalSpeed(jumpForce);
        }

        private void UpdateFloorHeight()
        {
            floorHeight = transform.localPosition.y;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + offset, radius);
            //Debug.DrawLine(transform.position + offset, transform.position + offset - transform.up * toleranceToFloor, Color.red);
        }
    }
}
