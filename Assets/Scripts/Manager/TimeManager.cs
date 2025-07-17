using System.Collections;
using UnityEngine;

namespace Tuleeeeee.Manager
{
    public class TimeManager
    {
        private static Coroutine hitStopCoroutine;

        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public static void ResumeGame()      
        {
            Time.timeScale = 1f;
        }

        public static void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        public static bool IsPaused()
        {
            return Time.timeScale == 0f;
        }

        public static void HitStop(MonoBehaviour mono, float duration, float slowDownFactor = 0f)
        {
            if (hitStopCoroutine != null)
            {
                mono.StopCoroutine(hitStopCoroutine); // Stop any previous hit stop
            }

            hitStopCoroutine = mono.StartCoroutine(HitStopCoroutine(duration, slowDownFactor));
        }

        private static IEnumerator HitStopCoroutine(float duration, float slowDownFactor)
        {
            Time.timeScale = slowDownFactor;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1f;
        }
    }
}