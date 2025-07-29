using UnityEngine;

namespace HighwayDash
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [Header("Lane Settings")]
        [SerializeField][Range(1f, 10f)] private int maxLanes = 3;
        [SerializeField][Range(1f, 10f)] private float laneDistance = 3f;
        [SerializeField][Range(0.01f, 1f)] private float laneChangeThreshold = 0.8f;

        [Header("Movement Settings")]
        [SerializeField][Range(1f, 100f)] private float forwardSpeed = 10f;
        [SerializeField][Range(1f, 100f)] private float laneChangeSpeed = 10f;
        [SerializeField][Range(1f, 100f)] private float jumpForce = 10f;
        [SerializeField][Range(1f, 100f)] private float gravity = 20f;

        private CharacterController cc;
        private Vector3 moveDirection = Vector3.zero;

        private int currentLane = 1; // Left = 0, Middle = 1, Right = 2
        private bool isLaneChanging = false;
        private float verticalVelocity = 0f;

        #endregion

        // ----------------------------------------------------------------------

        #region Main Methods

        private void Start()
        {
            cc = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ApplyGravity();
            Move();
        }

        #endregion

        // ----------------------------------------------------------------------

        #region Custom Methods

        private void ApplyGravity()
        {
            if (!cc.isGrounded)
                verticalVelocity -= gravity * Time.deltaTime;
            else if (verticalVelocity < 0)
                verticalVelocity = -1f;
        }

        private void Move()
        {
            Vector3 targetPosition = transform.position.z * Vector3.forward;
            targetPosition += Vector3.right * ((currentLane - 1) * laneDistance); // -1 for middle

            Vector3 lateralMove = Vector3.zero;
            lateralMove.x = (targetPosition - transform.position).x * laneChangeSpeed;

            moveDirection.x = lateralMove.x;
            moveDirection.y = verticalVelocity;
            moveDirection.z = forwardSpeed;

            float distanceX = targetPosition.x - transform.position.x;

            if (Mathf.Abs(distanceX) < laneChangeThreshold) isLaneChanging = false;

            cc.Move(moveDirection * Time.deltaTime);
        }

        public void ChangeLane(int lane)
        {
            if (!isLaneChanging)
            {
                int targetLane = Mathf.Clamp(currentLane + lane, 0, maxLanes - 1);
                if (targetLane != currentLane) currentLane = targetLane;
                isLaneChanging = true;
            }

            //if (cc.isGrounded && input.JumpPressed)
            //    verticalVelocity = jumpForce;
        }

        #endregion
    }
}
