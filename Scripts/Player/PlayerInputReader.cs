using UnityEngine;

namespace HighwayDash
{
    /// <summary>
    /// Reads input actions and forwards them to the PlayerMovement component.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInputReader : MonoBehaviour
    {
        #region Variables

        private InputSystem_Actions controls;
        private PlayerMovement playerMovement;

        #endregion

        // ----------------------------------------------------------------------

        #region Main Methods

        private void Awake()
        {
            controls = new InputSystem_Actions();

            // Bind input events to actions
            controls.Player.Jump.performed += ctx => TriggerJump();
            controls.Player.RightLane.performed += ctx => ChangeLane(1);
            controls.Player.LeftLane.performed += ctx => ChangeLane(-1);
        }

        private void Start() => playerMovement = GetComponent<PlayerMovement>();

        private void OnEnable() => controls.Enable();

        private void OnDisable() => controls.Disable();

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
