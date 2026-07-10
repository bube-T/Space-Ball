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
        RespondToKeys(); // Check for shortcut key inputs
    }

    // Handles shortcut keys: R restarts the level, Esc returns to the menu, G skips (debug builds only)
    void RespondToKeys()
    {
        if (Keyboard.current == null) return; // No keyboard connected

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadLevel(); // Quick restart for the player
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0); // Back to the main menu
        }
        else if (Debug.isDebugBuild && Keyboard.current.gKey.wasPressedThisFrame)
        {
            LoadNextLevel(); // Debug-only level skip
        }
    }

    // Detects collision with objects in the game
    private void OnCollisionEnter(Collision other)
    {
        if (hasCollided) return; // Prevent multiple collision triggers

        switch (other.gameObject.tag) // Check the tag of the collided object
        {
            case "Friendly":
                break; // No effect if colliding with friendly objects
            case "Finish":
                StartSuccess(); // Handle level completion
                break;
            default:
                StartCrash(); // Handle crashes with obstacles
                break;
        }
    }

    // Handles what happens when the player reaches the finish pad
    void StartSuccess()
    {
        StopLevelTimer(); // Stop the countdown so it can't restart the level mid-celebration
        hasCollided = true; // Prevent further collisions from triggering events
        audioSource.Stop(); // Stop any currently playing audio
        audioSource.PlayOneShot(successSFX); // Play success sound effect
        successParticles.Play(); // Play success particle effect
        GetComponent<Movement>().enabled = false; // Disable movement to prevent unwanted actions
        Invoke(nameof(LoadNextLevel), levelLoadDelay); // Load next level after delay
    }

    // Handles what happens when the player crashes
    void StartCrash()
    {
        StopLevelTimer(); // Stop the countdown while the crash sequence plays
        hasCollided = true; // Prevent further collisions from triggering events
        audioSource.Stop(); // Stop any currently playing audio
        audioSource.PlayOneShot(crashSFX); // Play crash sound effect
        crashParticles.Play(); // Play crash particle effect
        GetComponent<Movement>().enabled = false; // Disable movement to prevent further input
        Invoke(nameof(ReloadLevel), levelLoadDelay); // Reload level after delay
    }

    // Pauses the level countdown timer if one exists in the scene
    void StopLevelTimer()
    {
        LevelTimer timer = FindObjectOfType<LevelTimer>();
        if (timer != null)
        {
            timer.StopTimer();
        }
    }

    // Reloads the current level
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the active scene
    }

    // Loads the next level or returns to the menu after the last one
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0; // Beat the last level: return to the main menu
        }

        SceneManager.LoadScene(nextScene); // Load the next scene
    }
}
