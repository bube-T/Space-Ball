using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    public Transform player; // PlayerBall
    public float followSpeed = 5f; // Speed of movement
    public float positionLerpSpeed = 0.1f; // How the drones follows

    void Update()
    {
        if (player == null) return; // Prevent errors if no player is assigned

        // Get the ball’s X and Z position (but keep the drone's Y position)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Move smoothly towards the new position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}

