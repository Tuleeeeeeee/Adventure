using System;
using System.Collections;
using System.Collections.Generic;
using Tuleeeeee.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Tuleeeeee.Manager
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public event Action OnWin;
        [SerializeField] private CollectibleEventSO collectibleEventSO;

        #region  Tooltip
        [Tooltip("Populate with the dungeon level sciptable objects")]
        #endregion Tooltip
        [SerializeField] private List<LevelSO> levelList;
        #region Tooltip
        [Tooltip("Populate with starting the dungeon level for testing, first level =0")]
        #endregion Tooltip
        [SerializeField] private int currentLevelListIndex;
        private LevelSO currentLevel;
        private LevelSO previousLevel;
        private GameObject currentLevelInstance;

        private GameState gameState;
        private GameState previousGameState;

        private PlayerDetailsSO playerDetails;
        private Player player;
        private int totalFruits;
        private int fruitsCollected = 0;
        private GameObject[] cachedFruits;

        private void OnEnable()
        {
            collectibleEventSO.OnCollectibleCollected.AddListener(HandleFruitCollected);
        }

        private void OnDestroy()
        {
            collectibleEventSO.OnCollectibleCollected.RemoveListener(HandleFruitCollected);
        }
        public override void Init()
        {
            playerDetails = GameResources.Instance.currentPlayerSO.playerDetails;
            InstantiatePlayer();
        }
        private void InstantiatePlayer()
        {
            GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

            player = playerGameObject.GetComponent<Player>();

            player.Initialize(playerDetails);
        }

        private void Start()
        {
            previousGameState = GameState.GameStart;
            gameState = GameState.GameStart;

            currentLevel = levelList[currentLevelListIndex];
            previousLevel = levelList[currentLevelListIndex];
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.GameStart:
                    InitializeLevel(currentLevelListIndex);
                    ChangeState(GameState.Playing);
                    break;
                case GameState.Playing:
                    break;
                case GameState.LevelCompleted:
                    StartCoroutine(LevelCompleted());
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
                case GameState.GameWon:
                    StartCoroutine(GameWon());
                    break;
            }
        }

        private void InitializeLevel(int currentLevelListIndex)
        {
            if (currentLevelListIndex < 0 || currentLevelListIndex >= levelList.Count)
            {
                Debug.LogError("Invalid level index!");
                return;
            }

            currentLevelInstance = Instantiate(currentLevel.levelPrefab);

            Tilemap tilemap = currentLevelInstance.GetComponentInChildren<Tilemap>();
            if (tilemap != null)
            {
                // Get local bounds and convert to world bounds
                Bounds bounds = tilemap.localBounds;
                FitCameraToBounds(bounds, currentLevelInstance.transform);
            }

            // Move player to spawn position
            Vector3 playerPosition = currentLevel.playerSpawnPosition;
            player.transform.position = playerPosition;


            SpawnFruits(currentLevel);
            SpawnTraps(currentLevel);

        }

        private void SpawnFruits(LevelSO currentLevel)
        {
            List<GameObject> newFruits = new List<GameObject>();
            foreach (var group in currentLevel.collectiblesListToSpawn)
            {
                foreach (var pos in group.spawnPositions)
                {
                    Vector3 centerPos = pos + new Vector3(0f, 0.5f, 0f);
                    GameObject collectible = Instantiate(group.collectiblePrefab, centerPos, Quaternion.identity);
                    newFruits.Add(collectible);
                }
            }

            cachedFruits = newFruits.ToArray();
            totalFruits = cachedFruits.Length;
            fruitsCollected = 0;
        }
        private void SpawnTraps(LevelSO currentLevel)
        {
            foreach (var group in currentLevel.trapListToSpawn)
            {
                foreach (var pos in group.spawnPositions)
                {
                    Vector3 centerPos = pos + new Vector3(0.5f, 0.5f, 0f);
                    Instantiate(group.trapPrefab, pos, Quaternion.identity);
                }
            }
        }
        private IEnumerator LevelCompleted()
        {
            ChangeState(GameState.Playing);

            yield return new WaitForSeconds(2f);

            //    yield return StartCoroutine(Fade(0f, 1f, 2f, new Color(0f, 0f, 0f, 0.4f)));

            // Check if there are more levels
            currentLevelListIndex++;

            if (currentLevelListIndex > levelList.Count - 1)
            {
                Debug.Log("🎉 All levels completed!");
                ChangeState(GameState.GameWon);
                yield break;
            }

            ChangeLevel(levelList[currentLevelListIndex]);

            ClearLevel();

            InitializeLevel(currentLevelListIndex);
        }
        private IEnumerator GameWon()
        {
            Debug.Log("🎉 Game Won!");
            yield return new WaitForSeconds(2f);
        }

        public void ClearLevel()
        {
            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
                currentLevelInstance = null;
            }
        }

        private void FitCameraToBounds(Bounds bounds, Transform levelTransform)
        {
            Camera cam = Camera.main;

            // Convert local bounds center to world position
            Vector3 center = levelTransform.TransformPoint(bounds.center);
            cam.transform.position = new Vector3(center.x, center.y, cam.transform.position.z);

            // Get world bounds size
            Vector3 worldSize = Vector3.Scale(bounds.size, levelTransform.lossyScale);

            float verticalSize = worldSize.y / 2f;
            float horizontalSize = worldSize.x / (2f * cam.aspect);

            cam.orthographicSize = Mathf.Max(verticalSize, horizontalSize);

            Debug.Log($"📷 Camera adjusted to level bounds: size={cam.orthographicSize}, center={center}");
        }

        private void HandleFruitCollected(CollectibleEventArgs args)
        {
            if (args.collectibleType == CollectibleType.Fruit)
            {
                Debug.Log($"Fruit collected! Type: {args.collectibleType}, Value: {args.value}");
                fruitsCollected++;

                if (fruitsCollected >= totalFruits)
                {
                    Debug.Log("All fruits collected! Level completed.");
                    OnWin?.Invoke();
                    ChangeState(GameState.LevelCompleted);
                }
            }
        }
        private void ChangeLevel(LevelSO newlevel)
        {
            previousLevel = currentLevel;
            currentLevel = newlevel;
        }
        private void ChangeState(GameState newState)
        {
            previousGameState = gameState;
            gameState = newState;
        }
    }
}
