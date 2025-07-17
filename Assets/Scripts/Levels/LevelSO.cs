using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Scriptable Objects/Levels/New Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public string levelName;

    [Header("Level Setup")]
    public GameObject levelPrefab;

    [Header("Spawning")]
    public Vector3 playerSpawnPosition;

    [Header("Collectibles")]
    public List<CollectibleSpawnData> collectiblesListToSpawn;

    [Header("Traps")]
    public List<TrapSpawnData> trapListToSpawn;

    [Header("Eemies")]
    public List<EnemySpawnData> enemyListToSpawn;
}

[System.Serializable]
public class CollectibleSpawnData
{
    public GameObject collectiblePrefab;
    public List<Vector3> spawnPositions;
}
[System.Serializable]
public class TrapSpawnData
{
    public GameObject trapPrefab;
    public List<Vector3> spawnPositions;
}
[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public List<Vector3> spawnPositions;
}
