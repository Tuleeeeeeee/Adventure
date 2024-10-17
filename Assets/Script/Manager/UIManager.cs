using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Button nextButton;       // Reference to the Next button in the UI
    public Button previousButton;   // Reference to the Previous button in the UI

    [SerializeField]
    private GameObject winScreenUI;
    [SerializeField]
    private TextMeshProUGUI winMessage;

    public TransitionSettings transition;
    public float startDelay;
    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonVisibility();
    }
    private void FixedUpdate()
    {
        UpdateButtonVisibility();
    }
    public void NextLevel()
    {
        changeTimeScale();
        HideWinScreen();
        nextScene();
    }
    public void PreviousLevel()
    {
        changeTimeScale();
        HideWinScreen();
        prexScene();
    }
    public void RestartLevel()
    {

        changeTimeScale();
        HideWinScreen();
        restartScene();
    }
    private void changeTimeScale()
    {
        Time.timeScale = 1.0f;
    }
    private void restartScene()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload current scene by name
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, startDelay);
    }
    private void nextScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex + 1, transition, startDelay);
    }
    private void prexScene()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex - 1, transition, startDelay);
    }
    private void UpdateButtonVisibility()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Hide the Previous button if there is no previous level
        if (previousButton != null)
            previousButton.gameObject.SetActive(currentSceneIndex > 1);

        // Hide the Next button if there is no next level
        if (nextButton != null)
            nextButton.gameObject.SetActive(currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1);
    }
    private void OnEnable()
    {
        // Subscribe to the GameManager's OnWin event
        GameManager.Instance.OnWin += ShowWinScreen;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the GameManager's OnWin event
        GameManager.Instance.OnWin -= ShowWinScreen;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void HideWinScreen()
    {
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(false);  // Hide the win screen UI
        }
    }
    public void ShowUIWithDelay(float delay)
    {
        // Delay the call to ShowUI using Invoke
        Invoke("ShowUI", delay);
    }
    private void ShowUI()
    {
        // Show the UI after the delay
        transform.gameObject.SetActive(true);
    }
    // Reset the win screen and update buttons when the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.gameObject.SetActive(false);
        HideWinScreen();  // Hide the win screen when the scene loads
        UpdateButtonVisibility();
        SetActiveUIBasedOnScene(scene);
        ShowUIWithDelay(1);
    }
    private void SetActiveUIBasedOnScene(Scene currentScene)
    {
        // Set the active state based on the scene index
        bool isActive = currentScene.buildIndex != 0; // Active if not the first scene
        transform.GetChild(0).gameObject.SetActive(isActive);
    }
    // Method to show the Win Screen when the player wins
    private void ShowWinScreen()
    {
        Debug.Log("You won the game!");

        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);  // Show the win screen UI
        }

        if (winMessage != null)
        {
            winMessage.text = "You Win!";  // Optional: Display a message
        }

        Time.timeScale = 0f;  // Pause the game (optional)
    }
}
