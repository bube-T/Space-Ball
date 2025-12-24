using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsPanel; 
    public int firstLevelIndex = 1; // the first level index as it is in build settings

    void Start()
    {
        levelsPanel.SetActive(false); // Hide level selection at start
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelIndex); // Load the forst level
    }

    public void OpenLevelSelection()
    {
        levelsPanel.SetActive(true); // Show level selection panel
    }

    public void LoadSpecificLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); // Load the selected level
    }


    public void CloseLevelSelection()
    {
        levelsPanel.SetActive(false); // Hide level selection panel
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
        Debug.Log("Game Quit!"); // Debug message for testing
    }
}


