using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText; //  UI text
    [SerializeField] float displayDuration = 2f; // How long the text stays visible

    private static bool uiExists = false; // Prevent duplicate canavas

    void Awake()
    {
        if (!uiExists)
        {
            DontDestroyOnLoad(gameObject); // Keep UI when changing levels
            uiExists = true;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate UI
        }
    }

    void Start()
    {
        UpdateLevelText(); // Ensure correct level is displayed at the start
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded; // Update text when scene loads
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded; // Remove event listener
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLevelText(); // Update text when scene changes
    }

    void UpdateLevelText()
    {
        int gameLevel = SceneManager.GetActiveScene().buildIndex; // Get scene index
        int displayedLevel = gameLevel; // Adjust if Main Menu is Scene 0

        if (gameLevel == 0) // If Main Menu is the first scene
        {
            levelText.gameObject.SetActive(false); // Don't show level text on menu
            return;
        }

        levelText.text = "Level " + displayedLevel; // Corrected level numbering
        levelText.gameObject.SetActive(true);
        Invoke(nameof(HideLevelText), displayDuration); // Hide after a delay
    }

    void HideLevelText()
    {
        levelText.gameObject.SetActive(false); // Hide text
    }
}
