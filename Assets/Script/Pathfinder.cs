using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveConfig waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    void Awake()
    {
        // Safely find the EnemySpawner in the scene
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not found in the scene!");
        }
    }

    void Start()
    {
        // Ensure we have a valid current wave from the EnemySpawner
        if (enemySpawner != null)
        {
            waveConfig = enemySpawner.GetCurrentWave();
            if (waveConfig != null)
            {
                waypoints = waveConfig.GetWaypoints();
                if (waypoints != null && waypoints.Count > 0)
                {
                    transform.position = waypoints[waypointIndex].position;
                }
                else
                {
                    Debug.LogError("No waypoints found in the current wave!");
                }
            }
            else
            {
                Debug.LogError("Current wave configuration is null!");
            }
        }
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        // Ensure waypoints are valid before trying to follow the path
        if (waypoints != null && waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            // Check if the object reached the target position
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // Destroy the game object once all waypoints are reached
            Destroy(gameObject);
        }
    }
}
