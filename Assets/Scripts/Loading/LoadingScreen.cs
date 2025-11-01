using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private bool isFading;

    public Coroutine ShowLoading()
    {
        return StartCoroutine(Fade(0f, 1f, 1f, Color.black));
    }

    public Coroutine HideLoading()
    {
        return StartCoroutine(Fade(1f, 0f, 1f, Color.black));
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha, float duration, Color backgroundColor)
    {
        isFading = true;
        canvasGroup.alpha = startAlpha;

        Image image = canvasGroup.GetComponent<Image>();
        image.color = backgroundColor;

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, Mathf.Min(time / duration, 1f));
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        isFading = false;
    }
}
