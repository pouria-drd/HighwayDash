using UnityEngine;

namespace HighwayDash
{
    public class CameraFollow : MonoBehaviour
    {
        #region Variables

        [Header("Target Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new();

        [Header("Follow Settings")]
        [SerializeField][Range(1f, 100f)] private float followSpeed = 5f;
        [SerializeField] private bool followX = true;
        [SerializeField] private bool followY = true;
        [SerializeField] private bool followZ = true;

        #endregion

        // ----------------------------------------------------------------------

        #region Main Methods

        private void Start()
        {
            if (!target)
            {
                target = FindAnyObjectByType<Player>().transform;
                Debug.LogWarning("Target transform is null.");
            }

            if (!target)
            {
                Debug.LogError("Player not found.");
                return;
            }
        }

        private void LateUpdate()
        {
            Follow();
        }

        #endregion

        // ----------------------------------------------------------------------

        #region Custom Methods

        private void Follow()
        {
            if (target == null)
            {
                Debug.LogWarning("Target transform is null.");
                return;
            }

            Vector3 desiredPosition = target.position + offset;

            Vector3 currentPosition = transform.position;

            Vector3 newPosition = new Vector3(
                followX ? desiredPosition.x : currentPosition.x,
                followY ? desiredPosition.y : currentPosition.y,
                followZ ? desiredPosition.z : currentPosition.z
            );

            transform.position = Vector3.Lerp(currentPosition, newPosition, followSpeed * Time.deltaTime);
        }

        #endregion
    }

}
