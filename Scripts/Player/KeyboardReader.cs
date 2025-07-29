using UnityEngine;

namespace HighwayDash
{
    /// <summary>
    /// Reads input actions and forwards them to the PlayerMovement component.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    public class KeyboardReader : MonoBehaviour
    {
        #region Variables

        private PlayerMovement playerMovement;
        private InputSystem_Actions inputActions;

        #endregion

        // ----------------------------------------------------------------------

        #region Main Methods

        private void Awake()
        {
            inputActions = new InputSystem_Actions();

            // Bind input events to actions
            inputActions.Player.Jump.performed += ctx => TriggerJump();
            inputActions.Player.RightLane.performed += ctx => ChangeLane(1);
            inputActions.Player.LeftLane.performed += ctx => ChangeLane(-1);
        }

        private void Start() => playerMovement = GetComponent<PlayerMovement>();

        private void OnEnable() => inputActions.Enable();

        private void OnDisable() => inputActions.Disable();

        #endregion

        // ----------------------------------------------------------------------

        #region Custom Methods

        /// <summary>
        /// Requests a lane change from the PlayerMovement component.
        /// </summary>
        /// <param name="lane">Direction: -1 for left, 1 for right.</param>
        private void ChangeLane(int lane)
        {
            playerMovement.ChangeLane(lane);
        }

        /// <summary>
        /// Triggers the jump in PlayerMovement.
        /// </summary>
        private void TriggerJump()
        {
            playerMovement.Jump();
        }

        #endregion
    }
}
