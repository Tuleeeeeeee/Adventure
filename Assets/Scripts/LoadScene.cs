using EasyTransition;
using Tuleeeeee.Manager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneLevel : MonoBehaviour
{
    // Assign the level button in the Inspector
    public Button[] levelButtons;
    public Sprite[] levelImages;

    public TransitionSettings transition;
    public float startDelay;

    private void Awake()
    {
        ButtonToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        // Assign the onClick event for each button
        /*for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            ChangeButtonSprite(levelButtons[i], levelImages[i]);
            levelButtons[i].interactable = false;
        }*/


        Debug.Log(SceneManager.sceneCountInBuildSettings);
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            int index = i; // Capture the index
            ChangeButtonSprite(levelButtons[i], levelImages[i]);
            levelButtons[i].interactable = false;
            levelButtons[i].gameObject.SetActive(true);
            levelButtons[i].onClick.AddListener(() => SceneController.LoadSceneByIndex(index + 1, transition, startDelay));
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            levelButtons[i].interactable = true;
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

    private void ButtonToArray()
    {
        int childCount = transform.childCount;
        levelButtons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            levelButtons[i] = transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}