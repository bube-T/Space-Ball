using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelTime = 30f; // how long each level lasts
    [SerializeField] TextMeshProUGUI timerText; // levelTimer Text 

    private float timeRemaining;
    private bool timerRunning = true;

    void Start()
    {
        ResetTimer(); // Start the timer when the level loads
        
    }

    void Update()
    {
        if (!timerRunning) return; // Stop if timer is not running

        timeRemaining -= Time.deltaTime; // Decrease time

        if (timeRemaining <= 0)
        {
            RestartLevel(); // Restart level if time runs out
        }

        UpdateTimerUI(); // Update the timer display
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining) + "s"; // Display whole seconds
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload level
    }

    public void ResetTimer()
    {
        timeRemaining = levelTime; // Set the timer back to full time
        timerRunning = true;
    }

    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0; // Restart from first level if no more levels
        }

        SceneManager.LoadScene(nextScene);
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
        ResetTimer(); // Reset the timer when a new level loads
    }
}
