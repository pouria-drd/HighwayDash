using UnityEngine;

namespace HighwayDash
{
    /// <summary>
    /// Handles the player movement logic such as lane switching and jumping.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [Header("Lane Settings")]
        [SerializeField][Range(1f, 10f)] private int maxLanes = 3;
        [SerializeField][Range(1f, 10f)] private float laneDistance = 3f;
        [SerializeField][Range(1f, 100f)] private float laneChangeSpeed = 10f;
        [SerializeField][Range(0.1f, 2f)] private float laneChangeThreshold = 1f;

        [Header("Movement Settings")]
        [SerializeField][Range(1f, 100f)] private float gravity = 20f;
        [SerializeField][Range(1f, 100f)] private float forwardSpeed = 10f;
        [SerializeField][Range(1f, 100f)] private bool canJump = true;
        [SerializeField][Range(1f, 100f)] private float jumpForce = 10f;

        private CharacterController cc;
        private Vector3 moveDirection = Vector3.zero;

        private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
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

        /// <summary>
        /// Applies gravity to the player if airborne.
        /// </summary>
        private void ApplyGravity()
        {
            if (!cc.isGrounded)
                verticalVelocity -= gravity * Time.deltaTime;
            else if (verticalVelocity < 0)
                verticalVelocity = -1f; // Keeps the player grounded
        }

        /// <summary>
        /// Moves the player forward and handles lateral lane changes.
        /// </summary>
        private void Move()
        {
            // Determine the position in the correct lane
            Vector3 targetPosition = transform.position.z * Vector3.forward;
            targetPosition += Vector3.right * ((currentLane - 1) * laneDistance); // middle lane is index 1

            // Calculate horizontal movement
            Vector3 lateralMove = Vector3.zero;
            lateralMove.x = (targetPosition - transform.position).x * laneChangeSpeed;

            // Set final move direction
            moveDirection.x = lateralMove.x;
            moveDirection.y = verticalVelocity;
            moveDirection.z = forwardSpeed;

            // Check if the lane change is complete
            float distanceX = targetPosition.x - transform.position.x;
            if (Mathf.Abs(distanceX) < laneChangeThreshold) isLaneChanging = false;

            // Move the character
            cc.Move(moveDirection * Time.deltaTime);
        }

        /// <summary>
        /// Changes the current lane in the specified direction.
        /// </summary>
        /// <param name="lane">Direction: -1 for left, 1 for right.</param>
        public void ChangeLane(int lane)
        {
            if (!isLaneChanging)
            {
                int targetLane = Mathf.Clamp(currentLane + lane, 0, maxLanes - 1);
                if (targetLane != currentLane) currentLane = targetLane;
                isLaneChanging = true;
            }
        }

        /// <summary>
        /// Makes the player jump if grounded.
        /// </summary>
        public void Jump()
        {
            if (cc.isGrounded && canJump)
                verticalVelocity = jumpForce;
        }

        #endregion
    }
}
