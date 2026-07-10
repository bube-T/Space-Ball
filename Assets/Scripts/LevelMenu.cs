using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsPanel;
    public int firstLevelIndex = 1; // the first level index as it is in build settings

    void Start()
    {
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(false); // Hide level selection at start
        }
    }

    void Update()
    {
        // Escape closes the level selection panel
        if (levelsPanel != null && levelsPanel.activeSelf &&
            Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseLevelSelection();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelIndex); // Load the first level
    }

    public void OpenLevelSelection()
    {
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(true); // Show level selection panel
        }
    }

    public void LoadSpecificLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); // Load the selected level
    }

    public void CloseLevelSelection()
    {
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(false); // Hide level selection panel
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Also stop play mode in the editor
#endif
    }
}
