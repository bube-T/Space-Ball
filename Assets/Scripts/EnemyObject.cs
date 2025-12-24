using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private GameObject player; 
    public float detectionRange = 15f;
    public float moveSpeed = 5f;
    public float followHeight = 2f;
    public float stoppingDistance = 3f;
    public float returnDistance = 25f;

    private Vector3 startPosition;
    private enum State { Idle, Chasing, Returning }
    private State currentState = State.Idle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Auto-find the player
        startPosition = transform.position; // Save thr enemy's start position
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case State.Idle:
                if (distance < detectionRange)
                {
                    SwitchState(State.Chasing);
                }
                break;

            case State.Chasing:
                FollowPlayer();
                if (distance > returnDistance)
                {
                    SwitchState(State.Returning);
                }
                break;

            case State.Returning:
                ReturnToStart();
                if (Vector3.Distance(transform.position, startPosition) < 1f)
                {
                    SwitchState(State.Idle);
                }
                break;
        }
    }

    void SwitchState(State newState)
    {
        currentState = newState;
    }

    void FollowPlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 targetPosition = player.transform.position - directionToPlayer * stoppingDistance;
        targetPosition.y = player.transform.position.y + followHeight;

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ReturnToStart()
    {
        transform.position = Vector3.Lerp(transform.position, startPosition, moveSpeed * Time.deltaTime);
    }
}


 
//     public Transform player; // Reference to the player
//     public float detectionRange = 10f; // Distance before enemy starts chasing
//     public float moveSpeed = 3f; // Enemy movement speed
//     public float followDistance = 3f; // How far behind the player the enemy stays

//     private bool isChasing = false; // Tracks if enemy should follow

//     void Update()
//     {
//         float distance = Vector3.Distance(transform.position, player.position); // Get distance to player

//         if (distance < detectionRange)
//         {
//             isChasing = true; // Start chasing when player is close enough
//         }

//         if (isChasing)
//         {
//             FollowPlayer(); // Move towards the player but stay behind
//         }

//         if (distance > detectionRange * 1.5f)
//         {
//             isChasing = false; // Stop chasing if player gets too far
//         }
//     }

//     void FollowPlayer()
//     {
//         Vector3 directionToPlayer = (player.position - transform.position).normalized; // Get direction to player
//         Vector3 targetPosition = player.position - directionToPlayer * followDistance; // Position behind player

//         transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime); // Move smoothly
//     }
// }



