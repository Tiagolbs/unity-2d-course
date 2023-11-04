using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveConfigSo waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetShipSpeed() * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if ((Vector2)transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
