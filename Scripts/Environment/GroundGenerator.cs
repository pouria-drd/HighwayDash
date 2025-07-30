using HighwayDash;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and manages ground tilese.
/// </summary>
public class GroundGenerator : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("Ground prefab that contains a Ground component with a configured ExitPoint.")]
    [SerializeField] private Ground groundPrefab;

    [Header("Settings")]
    [SerializeField][Range(1, 10)] private int initialGrounds = 4;
    [SerializeField][Range(1, 10)] private int maxGrounds = 5;

    private Vector3 nextSpawnPosition = Vector3.zero;
    private readonly Queue<Ground> spawnedGrounds = new();

    #endregion

    // ----------------------------------------------------------------------

    #region Unity Methods

    private void Start()
    {
        for (int i = 0; i < initialGrounds; i++)
        {
            SpawnGround();
        }
    }

    private void OnEnable()
    {
        Player.OnBorderEnter += SpawnGround;
    }

    private void OnDisable()
    {
        Player.OnBorderEnter -= SpawnGround;
    }

    #endregion

    // ----------------------------------------------------------------------

    #region Custom Methods

    /// <summary>
    /// Instantiates a new ground tile and updates the spawn point using its ExitPoint.
    /// </summary>
    private void SpawnGround()
    {
        Ground newGround = Instantiate(groundPrefab, nextSpawnPosition, Quaternion.identity);

        // Parenting to keep hierarchy clean
        newGround.transform.parent = transform;

        // Set tile name based on current total count
        newGround.transform.name = $"Ground {spawnedGrounds.Count + 1}";

        if (newGround.ExitPoint != null)
        {
            Vector3 exit = newGround.ExitPoint.position;

            // Calculate half of the ground's Z length (Unity Plane = 10 units long)
            float groundLength = groundPrefab.transform.localScale.z * 10f;
            float halfLength = groundLength / 2f;

            // Update next spawn position: X and Z from ExitPoint, Y remains fixed
            nextSpawnPosition = new Vector3(exit.x, 0f, exit.z + halfLength);
        }
        else
        {
            Debug.LogWarning($"Ground prefab '{newGround.name}' is missing ExitPoint reference.");
        }

        spawnedGrounds.Enqueue(newGround);

        if (spawnedGrounds.Count > maxGrounds)
        {
            RemoveOldGround();
        }
    }

    /// <summary>
    /// Removes the oldest ground tile from the scene.
    /// </summary>
    private void RemoveOldGround()
    {
        if (spawnedGrounds.Count == 0) return;

        Ground oldGround = spawnedGrounds.Dequeue();
        Destroy(oldGround.gameObject);
    }

    #endregion
}
