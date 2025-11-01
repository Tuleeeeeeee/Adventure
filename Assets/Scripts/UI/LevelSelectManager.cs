using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectedManager : MonoBehaviour
{
    public Transform LevelParent;
    public GameObject levelButtonPrefab;
    public AreaData CurrentAreaData;

    public HashSet<string> UnlockLevelIDs = new HashSet<string>();

    private LevelSelectEventSystemHandler levelSelectEventSystemHandler;

    private List<GameObject> buttonObjects = new List<GameObject>();
    private Dictionary<GameObject, string> buttonLocations = new Dictionary<GameObject, string>();

    void Awake()
    {
        levelSelectEventSystemHandler = GetComponentInChildren<LevelSelectEventSystemHandler>();    
    }

    void Start()
    {
        LoadUnlockedLevels();
        CreateLevelButtons();
        gameObject.SetActive(false);
    }

    private void CreateLevelButtons()
    {
        for (int i = 0; i < CurrentAreaData.Levels.Count; i++)
        {
            GameObject buttonGO = Instantiate(levelButtonPrefab, LevelParent);
            buttonObjects.Add(buttonGO);

            RectTransform buttonRect = buttonGO.GetComponent<RectTransform>();

            buttonGO.name = CurrentAreaData.Levels[i].LevelID;
            CurrentAreaData.Levels[i].LevelButtonPrefab = buttonGO;

            LevelButton button = buttonGO.GetComponent<LevelButton>();
            button.SetUp(CurrentAreaData.Levels[i], UnlockLevelIDs.Contains(CurrentAreaData.Levels[i].LevelID));

            Selectable selectable = buttonGO.GetComponent<Selectable>();
            levelSelectEventSystemHandler.AddSelectable(selectable);
        }
        LevelParent.gameObject.SetActive(true);
        levelSelectEventSystemHandler.InitSelectable();
        levelSelectEventSystemHandler.SetFirstSlected();
    }

    private void LoadUnlockedLevels()
    {
        foreach (var level in CurrentAreaData.Levels)
        {
            if (level.IsUnlockedByDefault)
            {
                UnlockLevelIDs.Add(level.LevelID);
            }
        }
    }

    private void UnlockLevel(string levelID, LevelButton levelButton)
    {
        if (!UnlockLevelIDs.Contains(levelID))
        {
            UnlockLevelIDs.Add(levelID);
            levelButton.Unlock();
        }
    }
    
    [Button("Unlock Level")]
    public void UnlockNextLevel()
    {
        LevelButton levelButton = buttonObjects[1].GetComponent<LevelButton>();
        string levelToUnlock = levelButton.levelData.LevelID;
        UnlockLevel(levelToUnlock, levelButton);
    }
}