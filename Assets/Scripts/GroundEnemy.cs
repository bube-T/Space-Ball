using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public Transform[] waypoints; // Assign multiple waypoints in Inspector
    public float moveSpeed = 3f; // Speed of movement
    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Move towards the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

        // Check if enemy reached the waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.2f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length; // Move to next waypoint
        }
    }
}
