using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust; // Input action for moving upwards
    [SerializeField] InputAction rotation; // Input action for left/right rotation
    [SerializeField] float thrustStrength = 100f; // Controls how strong the thrust is
    [SerializeField] float rotationStrength = 100f; // Controls how fast the player rotates
    [SerializeField] AudioClip main; // Sound effect for thrust

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody for physics-based movement
        audioSource = GetComponent<AudioSource>(); // Get AudioSource to play sounds
    }

    private void OnEnable()
    {
        // Enable input actions when the script is active
        thrust.Enable();
        rotation.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions when the script is disabled (e.g. after a crash)
        thrust.Disable();
        rotation.Disable();
    }

    private void FixedUpdate()
    {
        // Handle movement logic in FixedUpdate for smoother physics
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed()) // If the thrust key (spacebar) is pressed
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); // Apply upward force

            if (!audioSource.isPlaying) // Play thrust sound only if it's not already playing
            {
                audioSource.PlayOneShot(main);
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the sound when thrust is released
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>(); // Get left/right input

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength); // Rotate left
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength); // Rotate right
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Temporarily freeze rotation to avoid physics interference
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime); // Rotate smoothly
        rb.freezeRotation = false; // Unfreeze rotation so physics can work normally
    }
}
