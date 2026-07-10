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
        startPosition = transform.position; // Save the enemy's start position
    }

    void Update()
    {
        if (player == null)
        {
            // Player may not exist yet (or was destroyed) - try to find it again
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

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
