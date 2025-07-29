using UnityEngine;

namespace HighwayDash
{
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

            controls.Player.RightLane.performed += ctx => ChangeLane(1);
            controls.Player.LeftLane.performed += ctx => ChangeLane(-1);

            //controls.Player.Jump.performed += _ => JumpPressed = true;
            //controls.Player.Jump.canceled += _ => JumpPressed = false;
        }

        private void Start() => playerMovement = GetComponent<PlayerMovement>();

        private void OnEnable() => controls.Enable();
        private void OnDisable() => controls.Disable();

        #endregion

        // ----------------------------------------------------------------------

        #region Custom Methods

        private void ChangeLane(int lane)
        {
            playerMovement.ChangeLane(lane);
        }

        #endregion
    }
}
