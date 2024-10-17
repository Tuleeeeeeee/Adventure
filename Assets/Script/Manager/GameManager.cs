
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    private int totalFruits; // Total fruits in the level
    private int fruitsCollected = 0; // Fruits collected by the player

    public Projectile projectile;

    public event Action OnWin;
    public override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }

    // This function is called every time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the collected fruits for the new scene
        if(scene.buildIndex == 14) SetUpPool();
        ResetFruits();
    }
    // Initialize the game level
    private void InitializeLevel()
    {
        totalFruits = GameObject.FindGameObjectsWithTag("Fruit").Length; // Count all collectible objects in the level
        Debug.Log($"Total Fruits in the level: {totalFruits}");
    }
    private void ResetFruits()
    {
        fruitsCollected = 0;
        InitializeLevel(); // Recount fruits for the new level
    }
    private void OnEnable()
    {
        Fruit.OnCollectiblesCollected += HandleFruitCollected;
    }
    private void OnDisable()
    {
        Fruit.OnCollectiblesCollected -= HandleFruitCollected;
    }
    private void HandleFruitCollected(int value, Fruit.CollectibleType type)
    {
        if (type == Fruit.CollectibleType.Fruit)
        {
            Debug.Log($"Fruit collected! Incrementing score by {value}");
            fruitsCollected++;

            if (fruitsCollected >= totalFruits)
            {
                Invoke(nameof(winGame), 0.5f);
            }
        }
    }
    private void SetUpPool()
    {
        ObjectPool.SetupPool(projectile, 10, "bullet");
    }
    private void winGame()
    {
        Debug.Log("You collected all fruits! You win!");
        OnWin?.Invoke();
    }


}
