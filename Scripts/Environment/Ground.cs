using UnityEngine;

namespace HighwayDash
{
    /// <summary>
    /// Holds reference to the ground's exit point (e.g., border).
    /// </summary>
    public class Ground : MonoBehaviour
    {
        [Tooltip("The position where the next ground should spawn (usually the Border).")]
        [SerializeField] private Transform exitPoint;

        /// <summary>
        /// Public getter for the exit point.
        /// </summary>
        public Transform ExitPoint => exitPoint;
    }
}
