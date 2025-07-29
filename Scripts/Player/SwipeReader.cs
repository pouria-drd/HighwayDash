using UnityEngine;
using UnityEngine.InputSystem;

namespace HighwayDash
{
    /// <summary>
    /// Detects swipe gestures on both touchscreens and mouse input.
    /// Supports swipe up, left, and right to control player movement.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    public class SwipeReader : MonoBehaviour
    {
        [Header("Swipe Settings")]
        [Tooltip("Minimum distance (in pixels) for a swipe to be considered valid.")]
        [SerializeField][Range(1f, 100f)] private float swipeThreshold = 50f;

        private PlayerMovement playerMovement;

        // Starting position of the touch or mouse press
        private Vector2 startTouchPosition;

        // Ending position of the touch or mouse release
        private Vector2 endTouchPosition;

        // Tracks if a swipe gesture is currently active
        private bool isSwiping = false;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Touchscreen.current != null)
            {
                HandleTouchInput();
            }
            else if (Mouse.current != null)
            {
                HandleMouseInput();
            }
        }

        /// <summary>
        /// Handles touch input: starts swipe on press, ends on release.
        /// </summary>
        private void HandleTouchInput()
        {
            var touch = Touchscreen.current.primaryTouch;

            // Detect swipe start
            if (touch.press.wasPressedThisFrame)
            {
                startTouchPosition = touch.position.ReadValue();
                isSwiping = true;
            }
            // Detect swipe end
            else if (touch.press.wasReleasedThisFrame && isSwiping)
            {
                endTouchPosition = touch.position.ReadValue();
                DetectSwipe();
                isSwiping = false;
            }
        }

        /// <summary>
        /// Handles mouse input similarly to touch input.
        /// </summary>
        private void HandleMouseInput()
        {
            // Detect swipe start
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                startTouchPosition = Mouse.current.position.ReadValue();
                isSwiping = true;
            }
            // Detect swipe end
            else if (Mouse.current.leftButton.wasReleasedThisFrame && isSwiping)
            {
                endTouchPosition = Mouse.current.position.ReadValue();
                DetectSwipe();
                isSwiping = false;
            }
        }

        /// <summary>
        /// Calculates the swipe direction based on start and end positions.
        /// Invokes appropriate player movement commands for valid swipes.
        /// </summary>
        private void DetectSwipe()
        {
            Vector2 delta = endTouchPosition - startTouchPosition;

            // Ignore if swipe distance is too small
            if (delta.magnitude < swipeThreshold)
                return;

            // Horizontal swipe: left or right
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    playerMovement.ChangeLane(1);
                }
                else
                {
                    playerMovement.ChangeLane(-1);
                }
            }
            // Vertical swipe: only up is handled here
            else
            {
                if (delta.y > 0)
                {
                    playerMovement.Jump();
                }
            }
        }
    }
}
