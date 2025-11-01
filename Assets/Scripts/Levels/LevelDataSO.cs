using UnityEngine;


[CreateAssetMenu(fileName = "New Level Data UI", menuName = "Scriptable Objects/Level/Level DataUI")]
public class LevelDataSO : ScriptableObject
{
    [Header("Level Stats")]
    public string LevelID;
    [Tooltip("For Starting Levels")] public bool IsUnlockedByDefault = false;
    [Tooltip("Level Index Start At 0")] public int levelIndex;

    [Header("Level Display Info")]
    public Sprite LevelThumbnail;

    public GameObject LevelButtonPrefab;
}
