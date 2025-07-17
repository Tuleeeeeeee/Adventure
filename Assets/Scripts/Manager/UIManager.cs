using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tuleeeeee.Manager
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        public Button nextButton; // Reference to the Next button in the UI
        public Button previousButton; // Reference to the Previous button in the UI

        [SerializeField] private GameObject winScreenUI;


        [SerializeField] private TransitionSettings transition;
        [SerializeField] private float startDelay;

        void Start()
        {
            UpdateButtonVisibility();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #region SceneChange

        public void NextLevel()
        {
            ResetGameState();
            SceneController.LoadNextScene(transition, startDelay);
        }

        public void PreviousLevel()
        {
            ResetGameState();
            SceneController.LoadPreviousScene(transition, startDelay);
        }

        public void RestartLevel()
        {
            ResetGameState();
            SceneController.RestartScene(transition, startDelay);
        }

        public void StartLevel() //Start Current Level
        {
            int currentLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            SceneController.LoadSceneByIndex(currentLevel, transition, startDelay);
        }

        #endregion

        private void ResetGameState()
        {
            TimeManager.ResumeGame();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdateButtonVisibility();
            SetActiveUIBasedOnScene(scene);
            ShowUIWithDelay(1);
        }

        #region UIHanle

        private void UpdateButtonVisibility()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (previousButton != null)
                previousButton.gameObject.SetActive(currentSceneIndex > 1);

            if (nextButton != null)
                nextButton.gameObject.SetActive(currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1);
        }
        private void SetActiveUIBasedOnScene(Scene currentScene)
        {
            // Set the active state based on the scene index
            bool isActive = currentScene.buildIndex != 0; // Active if not the first scene
            transform.GetChild(0).GetChild(0).gameObject.SetActive(isActive);
        }
        private void ShowUIWithDelay(float delay)
        {
            Invoke(nameof(ShowUI), delay);
        }
        private void ShowUI()
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        #endregion
    }
}