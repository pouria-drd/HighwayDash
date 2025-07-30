using System;
using UnityEngine;

namespace HighwayDash
{
    [RequireComponent(typeof(KeyboardReader))]
    [RequireComponent(typeof(SwipeReader))]
    public class Player : MonoBehaviour
    {

        #region Variables

        [Header("Ground Generator")]
        // Action for border trigger
        public static Action OnBorderEnter;
        public string borderTag = "Border";

        #endregion

        // ----------------------------------------------------------------------

        #region Main Methods

        private void OnTriggerEnter(Collider other)
        {
            // Trigger enter event if the collider has the border tag and if there are subscribers
            if (other.CompareTag(borderTag)) OnBorderEnter?.Invoke();
        }

        #endregion

        // ----------------------------------------------------------------------

        #region Custom Methods


        #endregion
    }
}
