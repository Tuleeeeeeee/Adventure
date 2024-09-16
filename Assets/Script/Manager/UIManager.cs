using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Button nextButton;       // Reference to the Next button in the UI
    public Button previousButton;
    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonVisibility();
    }
   
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
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
}
