using System.Collections.Generic;
using NaughtyAttributes;
using Tuleeeeee.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tuleeeeee.Manager
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [Foldout("Events")][SerializeField] private CollectibleEventSO collectibleEventSO;
        [Foldout("Events")][SerializeField] private GameEventSO nextLevelEvent;
        [Foldout("Events")][SerializeField] private GameEventSO prevLevelEvent;
        [Foldout("Events")][SerializeField] private GameEventSO restartEvent;
        [Foldout("Events")][SerializeField] private IntIntGameEventSO levelChangedEvent;
        [Foldout("Events")][SerializeField] private FloatGameEvent levelCompleteEvent;
        [Foldout("Events")][SerializeField] private FloatGameEvent levelWonEvent;
        [Foldout("Events")][SerializeField] private FloatGameEvent timeChangeEvent;
        [Foldout("Events")][SerializeField] private IntGameEventSO selectedLevelEvent;
        [Foldout("Events")][SerializeField] private GameStateGameEventSO gameStateChangeEvent;

        #region  Tooltip
        [Tooltip("Populate with the level sciptable objects")]
        #endregion Tooltip
        [SerializeField] private List<LevelSO> levelList;

        #region Tooltip
        [Tooltip("Populate level for testing, first level =0")]
        #endregion Tooltip
        [SerializeField] private int currentLevelListIndex;

        #region Tooltip
        [Tooltip("Populate level Builder")]
        #endregion Tooltip        
        [SerializeField] private LevelBuilder levelBuilder;

        #region Tooltip
        [Tooltip("Populate loading screen")]
        #endregion Tooltip
        [SerializeField] private LoadingScreen loadingScreen;

        private LevelSO currentLevel;

        private GameObject currentLevelInstance;

        private GameState currentGameState;
        private GameState previousGameState;

        private PlayerDetailsSO playerDetails;
        private Player player;

        private int totalFruits;
        private int fruitsCollected = 0;

        private float elapsedTime;
        private bool isRunning;

        private void OnEnable()
        {
            collectibleEventSO.OnCollectibleCollected.AddListener(OnFruitCollected);
            nextLevelEvent.RegisterListener(OnLoadNextLevel);
            prevLevelEvent.RegisterListener(OnLoadPreviousLevel);
            restartEvent.RegisterListener(OnRestartLevel);
            selectedLevelEvent.RegisterListener(LoadLevelByIndex);
        }

        private void OnDisable()
        {
            collectibleEventSO.OnCollectibleCollected.RemoveListener(OnFruitCollected);
            nextLevelEvent.UnregisterListener(OnLoadNextLevel);
            prevLevelEvent.UnregisterListener(OnLoadPreviousLevel);
            restartEvent.UnregisterListener(OnRestartLevel);
            selectedLevelEvent.UnregisterListener(LoadLevelByIndex);
        }

        public override void Init()
        {
            playerDetails = GameResources.Instance.currentPlayerSO.playerDetails;
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            player = Instantiate(playerDetails.playerPrefab).GetComponent<Player>();

            player.Initialize(playerDetails);
        }

        private void Start()
        {
            previousGameState = GameState.GameStart;
            currentGameState = GameState.GameStart;
            currentLevel = levelList[currentLevelListIndex];

            levelChangedEvent.Raise((currentLevelListIndex, levelList.Count - 1));
        }

        private void Update()
        {
            switch (currentGameState)
            {
                case GameState.GameStart:
                    InitializeLevel(currentLevelListIndex);
                    loadingScreen.HideLoading();
                    StartLevel();
                    ChangeState(GameState.Playing);
                    break;
                case GameState.Playing:
                    break;
                case GameState.LevelCompleted:
                    break;
                case GameState.Restart:
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
                case GameState.GameWon:
                    break;
            }
        }

        private void OnFruitCollected(CollectibleEventArgs args)
        {
            if (args.collectibleType == CollectibleType.Fruit)
            {
                fruitsCollected++;

                if (fruitsCollected >= totalFruits)
                {
                    float elapsedTime = GetElapsedTime();
                    isRunning = false;
                    if (currentLevelListIndex >= levelList.Count - 1)
                    {
                        levelWonEvent.Raise(elapsedTime);
                        ChangeState(GameState.GameWon);
                    }
                    else
                    {
                        levelCompleteEvent.Raise(elapsedTime);
                        ChangeState(GameState.LevelCompleted);
                    }
                }
            }
        }

        private void InitializeLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levelList.Count)
            {
#if UNITY_EDITOR
                Debug.LogError("Invalid level index!");
#endif
                return;
            }

            currentLevel = levelList[levelIndex];

            levelChangedEvent.Raise((currentLevelListIndex, levelList.Count - 1));

            levelBuilder.LoadLevel(currentLevel);

            SetPlayerPosition(currentLevel);

            GameObject currentLevelInstance = levelBuilder.CurrentLevelInstance;

            SetCameraSize(currentLevelInstance);

            totalFruits = levelBuilder.Fruits.Count;
            fruitsCollected = 0;
        }

        public void OnLoadNextLevel()
        {
            if (currentLevelListIndex + 1 >= levelList.Count) return;
            using (new LoadingScope(loadingScreen))
            {
                currentLevelListIndex++;
                InitializeLevel(currentLevelListIndex);
                ChangeState(GameState.Playing);
            }
            StartLevel();
        }

        public void OnLoadPreviousLevel()
        {
            if (currentLevelListIndex - 1 < 0) return;

            using (new LoadingScope(loadingScreen))
            {
                currentLevelListIndex--;
                InitializeLevel(currentLevelListIndex);
                ChangeState(GameState.Playing);
            }
            StartLevel();
        }

        public void OnRestartLevel()
        {
            using (new LoadingScope(loadingScreen))
            {
                levelBuilder.ResetFruits();
                InitializeLevel(currentLevelListIndex);
                ChangeState(GameState.Playing);
            }
            StartLevel();
        }

        public void LoadLevelByIndex(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levelList.Count)
            {
#if UNITY_EDITOR
                Debug.LogError($"❌ Invalid level index: {levelIndex}");
#endif
                return;
            }

            using (new LoadingScope(loadingScreen))
            {
                currentLevelListIndex = levelIndex;
                InitializeLevel(currentLevelListIndex);
                ChangeState(GameState.Playing);
            }

            StartLevel();
        }

        public void StartLevel()
        {
            elapsedTime = 0f;
            isRunning = true;
            FunctionUpdater.StopUpdaterWithName("LevelTimer");
            FunctionUpdater.Create(() =>
            {
                if (!isRunning) return true; // stop updater
                elapsedTime += Time.deltaTime;
                timeChangeEvent.Raise(elapsedTime);
                return false;
            }, "LevelTimer");
        }

        public float GetElapsedTime()
        {
            return elapsedTime;
        }

        private void SetPlayerPosition(LevelSO currentLevel)
        {
            player.transform.position = currentLevel.playerSpawnPosition;
            player.ResetTrail();
            player.StateManager.ChangeState(player.AppearState);
        }

        private void SetCameraSize(GameObject currentLevelInstance)
        {
            Tilemap tilemap = currentLevelInstance.GetComponentInChildren<Tilemap>();
            if (tilemap != null)
            {
                Bounds bounds = tilemap.localBounds;
                FitCameraToBounds(bounds, currentLevelInstance.transform);
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

        public void ChangeState(GameState newState)
        {
            if (newState != currentGameState)
            {
                previousGameState = currentGameState;
                currentGameState = newState;
                gameStateChangeEvent.Raise(currentGameState);
            }
        }
    }
}
