using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // How far the object moves
    [SerializeField] float speed; // Speed of the movement
    Vector3 startPosition; // Where the object starts
    Vector3 endPosition; // Where the object moves to
    float movementFactor; // Controls movement progrss

    void Start()
    {
        startPosition = transform.position; // Set the starting position
        endPosition = startPosition + movementVector; // Calculate the end position
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f); // Move back and forth over time
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor); // Smooth transition between start and end
    }
}
