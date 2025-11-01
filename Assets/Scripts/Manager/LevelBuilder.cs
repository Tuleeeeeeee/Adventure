using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    private GameObject currentLevelInstance;
    private readonly List<GameObject> fruitsList = new();
    private readonly List<GameObject> trapsList = new();

    public void LoadLevel(LevelSO level)
    {
        ClearLevel();
        currentLevelInstance = Instantiate(level.levelPrefab);

        SpawnFruits(level);
        SpawnTraps(level);
    }

    private void SpawnFruits(LevelSO level)
    {
        fruitsList.Clear();
        foreach (var group in level.collectiblesListToSpawn)
        {
            foreach (var pos in group.spawnPositions)
            {
                var fruit = Instantiate(group.collectiblePrefab, pos, Quaternion.identity, currentLevelInstance.transform);
                fruitsList.Add(fruit);
            }
        }
    }

    private void SpawnTraps(LevelSO level)
    {
        trapsList.Clear();
        foreach (var group in level.trapListToSpawn)
        {
            foreach (var pos in group.spawnPositions)
            {
                var trap = Instantiate(group.trapPrefab, pos, Quaternion.identity, currentLevelInstance.transform);
                trapsList.Add(trap);
            }
        }
    }

    private void ClearLevel()
    {
        if (currentLevelInstance != null)
            Destroy(currentLevelInstance);

        fruitsList.Clear();
        trapsList.Clear();
    }

    public void ResetFruits()
    {
        foreach (var fruit in fruitsList)
        {
            if (fruit != null)
            {
                fruit.GetComponent<Collider2D>().enabled = true;
                fruit.SetActive(true);
            }
        }
    }

    public GameObject CurrentLevelInstance => currentLevelInstance;
    public List<GameObject> Fruits => fruitsList;
}
