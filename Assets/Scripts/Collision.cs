using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; // Delay before loading the next level or reloading
    [SerializeField] AudioClip successSFX; // Sound effect for level completion
    [SerializeField] AudioClip crashSFX;   // Sound effect for crashing
    [SerializeField] ParticleSystem successParticles; // Particle effect for success
    [SerializeField] ParticleSystem crashParticles;   // Particle effect for crashing

    AudioSource audioSource;
    bool hasCollided = false; // Flag to track if a collision has already happened

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component for sound playback
    }

    private void Update()
    {
        DebugKeysResponse(); // Check for debug key inputs
    }

    // Handles debug key presses for quick level navigation
    void DebugKeysResponse()
    {
    if (Keyboard.current.gKey.wasPressedThisFrame) // Ensures G key is only detected once per press
    {
        loadNextlevel();
    }
    }


    // Detects collisiom with objects in the game
    private void OnCollisionEnter(Collision other)
    {
        if (hasCollided) return; // Prevent multiple collision triggers

        switch (other.gameObject.tag) // Check the tag of the collided object
        {
            case "Friendly":
                Debug.Log("we good"); // No effect if colliding with friendly objects
                break;
            case "Finish":
                StartSuccess(); // Handle level completion
                break;
            default:
                StartCrash(); // Handle crashes with obstacles
                break;
        }
    }

    

    // Handles what happens whem the player reaches the finish pad
    void StartSuccess()
    {
        FindObjectOfType<LevelTimer>(); // Reset the timer for the next level
        hasCollided = true; // Prevent further collisions from triggering events
        audioSource.Stop(); // Stop any currently playing audio
        audioSource.PlayOneShot(successSFX); // Play success sound effect
        successParticles.Play(); // Play success particle effect
        GetComponent<Movement>().enabled = false; // Disable movement to prevent unwanted actions
        Invoke("loadNextlevel", levelLoadDelay); // Load next level after delay
    }

    // Handles what happens when the player crashes
    void StartCrash()
    {
        hasCollided = true; // Prevent further collisions from triggering events
        audioSource.Stop(); // Stop any currently playing audio
        audioSource.PlayOneShot(crashSFX); // Play crash sound effect
        crashParticles.Play(); // Play crash particle effect
        GetComponent<Movement>().enabled = false; // Disable movement to prevent further input
        Invoke("ReloadLevel", levelLoadDelay); // Reload level after delay
    }

    // Reloads the current level
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the active scene
    }

    // Loads the next levrl or loops back to the first level if at the last one
    void loadNextlevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0; // If it's the last level, restart from level 0
        }

        SceneManager.LoadScene(nextScene); // Load the next scene
    }
}

