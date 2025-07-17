using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tuleeeeee.Manager
{
    public class SceneController : MonoBehaviour
    {
        public static void LoadSceneByIndex(int sceneIndex, TransitionSettings transitionSettings, float startDelay)
        {
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                TransitionManager.Instance().Transition(sceneIndex, transitionSettings, startDelay);
            }
            else
            {
                Debug.LogWarning($"Invalid scene index: {sceneIndex}");
            }
        }

        public static void LoadNextScene(TransitionSettings transitionSettings, float startDelay)
        {
            LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1, transitionSettings, startDelay);
        }

        public static void LoadPreviousScene(TransitionSettings transitionSettings, float startDelay)
        {
            LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex - 1, transitionSettings, startDelay);
        }

        public static void LoadSceneByName(string sceneName, TransitionSettings transitionSettings, float startDelay)
        {
            TransitionManager.Instance().Transition(sceneName, transitionSettings, startDelay);
        }

        public static void RestartScene(TransitionSettings transitionSettings, float startDelay)
        {
            LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex, transitionSettings, startDelay);
        }
    }
}