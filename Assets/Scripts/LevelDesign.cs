using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText; //  UI text
    [SerializeField] float displayDuration = 2f; // How long the text stays visible

    private static bool uiExists = false; // Prevent duplicate canvas

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
        if (scene.buildIndex == 0)
        {
            // Back at the main menu: remove the persistent level HUD entirely
            // so no timer or level text carries over onto the menu.
            uiExists = false;
            Destroy(gameObject);
            return;
        }

        UpdateLevelText(); // Update text when scene changes
    }

    void UpdateLevelText()
    {
        if (levelText == null) return;

        int gameLevel = SceneManager.GetActiveScene().buildIndex; // Get scene index

        if (gameLevel == 0) // Main Menu is the first scene
        {
            levelText.gameObject.SetActive(false); // Don't show level text on menu
            return;
        }

        CancelInvoke(nameof(HideLevelText)); // Restart the hide countdown on every level change
        levelText.text = "Level " + gameLevel;
        levelText.gameObject.SetActive(true);
        Invoke(nameof(HideLevelText), displayDuration); // Hide after a delay
    }

    void HideLevelText()
    {
        if (levelText != null)
        {
            levelText.gameObject.SetActive(false); // Hide text
        }
    }
}
