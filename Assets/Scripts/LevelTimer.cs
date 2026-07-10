using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelTime = 30f; // how long each level lasts
    [SerializeField] TextMeshProUGUI timerText; // levelTimer Text
    [SerializeField] float lowTimeWarning = 10f; // when the timer turns red

    private float timeRemaining;
    private bool timerRunning = true;
    private Color normalColor;

    void Start()
    {
        if (timerText != null)
        {
            normalColor = timerText.color; // Remember the original text colour
        }
        ResetTimer(); // Start the timer when the level loads
    }

    void Update()
    {
        if (!timerRunning) return; // Stop if timer is not running

        timeRemaining -= Time.deltaTime; // Decrease time

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false; // Stop the timer so the restart only triggers once
            UpdateTimerUI();
            RestartLevel(); // Restart level if time runs out
            return;
        }

        UpdateTimerUI(); // Update the timer display
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining) + "s"; // Display whole seconds
        // Turn red when time is nearly up so the player gets a clear warning
        timerText.color = (timeRemaining <= lowTimeWarning) ? Color.red : normalColor;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload level
    }

    public void ResetTimer()
    {
        timeRemaining = levelTime; // Set the timer back to full time
        timerRunning = true;
        UpdateTimerUI();
    }

    // Called when the level ends (success or crash) so the countdown can't restart the level
    public void StopTimer()
    {
        timerRunning = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for level changes
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Stop listening
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            // Main menu: no countdown should run or be visible
            StopTimer();
            if (timerText != null) timerText.gameObject.SetActive(false);
            return;
        }

        if (timerText != null) timerText.gameObject.SetActive(true);
        ResetTimer(); // Reset the timer when a new level loads
    }
}
