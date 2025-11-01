using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelDataSO levelData { get; private set; }

    private Button button;
    private Image thumbnailImage;

    public Color ReturnColor { get; private set; }

    [SerializeField] private IntGameEventSO selectedLevelEvent;

    void Awake()
    {
        button = GetComponent<Button>();
        thumbnailImage = GetComponent<Image>();
        ReturnColor = Color.gray; // Default color
    }

    public void SetUp(LevelDataSO levelData, bool isUnlocked)
    {
        this.levelData = levelData;
        thumbnailImage.sprite = levelData.LevelThumbnail;
        button.interactable = isUnlocked;

        if (isUnlocked)
        {
            button.onClick.AddListener(LoadLevel);
            ReturnColor = Color.white;
            thumbnailImage.color = ReturnColor; // Normal color for unlocked levels
        }
        else
        {
            ReturnColor = Color.gray;
            thumbnailImage.color = ReturnColor; // Gray out for locked levels
        }
    }

    public void Unlock()
    {
        button.interactable = true;
        button.onClick.AddListener(LoadLevel);
        ReturnColor = Color.white;
        thumbnailImage.color = ReturnColor; // Change to normal color when unlocked
    }

    private void LoadLevel()
    {
        selectedLevelEvent.Raise(levelData.levelIndex);
    }
}