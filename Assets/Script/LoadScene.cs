using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Assign the level button in the Inspector
    public Button[] levelButtons;
    public Sprite[] levelImages;

    public TransitionSettings transition;
    public float startDelay;
    private void Start()
    {
        // Assign the onClick event for each button
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i; // Capture the index
            ChangeButtonSprite(levelButtons[i], levelImages[i]);
            levelButtons[i].onClick.AddListener(() => LoadSceneByIndex(index + 1));
        }
    }
    public void ChangeButtonSprite(Button button, Sprite sprite)
    {
        // Get the Image component from the button
        Image buttonImage = button.GetComponent<Image>();

        // Check if the Image component exists
        if (buttonImage != null)
        {
            // Assign the new sprite to the Image component
            buttonImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"No Image component found on the button: {button.name}");
        }
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex);
        TransitionManager.Instance().Transition(sceneIndex, transition, startDelay);
        deactivateAndReset();
    }
    public void LoadSceneByName(string sceneName)
    {
        // SceneManager.LoadScene(sceneName);
        TransitionManager.Instance().Transition(sceneName, transition, startDelay);
        deactivateAndReset();
    }
    private void deactivateAndReset()
    {
        Time.timeScale = 1.0f;
        transform.gameObject.SetActive(false);
    }
}
