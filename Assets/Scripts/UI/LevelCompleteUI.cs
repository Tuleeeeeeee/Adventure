using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button levelSelectButton;

    [Header("Events")]
    [SerializeField] private GameEventSO nextLevelEvent;
    [SerializeField] private GameEventSO restartLevelEvent;
    [SerializeField] private GameEventSO levelSelectEvent;
    [SerializeField] private FloatGameEvent levelCompleteEvent;
    [SerializeField] private FloatGameEvent gameWonEvent;

    [Header("Stars UI")]
    [SerializeField] private Image[] starImagesPosition;
    [SerializeField] private GameObject starPrefab;

    [Tooltip("Time thresholds in seconds: index 0 = 3 stars, 1 = 2 stars, else 1 star")]
    [SerializeField] private float[] timeThresholds = { 30f, 60f };

    [Header("Pool Settings")]
    [SerializeField] private int starPoolSize = 3;
    private const string STAR_POOL_KEY = "LevelCompleteStars";
    private Image[] animatedStars;
    private List<GameObject> currentStarInstances = new List<GameObject>();

    private DynamicEventSystemHandler buttonEventSystemHandler;

    void Start()
    {
        buttonEventSystemHandler = GetComponentInChildren<DynamicEventSystemHandler>();
        levelCompleteScreen.gameObject.SetActive(false);
        ObjectPool.SetupPool(starPrefab.GetComponent<Image>(), starPoolSize, STAR_POOL_KEY);
        buttonEventSystemHandler.AddSelectable(nextButton);
        buttonEventSystemHandler.AddSelectable(restartButton);
        buttonEventSystemHandler.AddSelectable(levelSelectButton);
        buttonEventSystemHandler.InitSelectable();
    }

    void OnEnable()
    {
        nextButton.onClick.AddListener(() => nextLevelEvent.Raise());
        restartButton.onClick.AddListener(() => restartLevelEvent.Raise());
        levelSelectButton.onClick.AddListener(() => levelSelectEvent.Raise());

        levelCompleteEvent.RegisterListener(OnLevelComplete);
        gameWonEvent.RegisterListener(OnGameWon);
    }

    void OnDisable()
    {
        nextButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        levelSelectButton.onClick.RemoveAllListeners();

        levelCompleteEvent.UnregisterListener(OnLevelComplete);
        gameWonEvent.UnregisterListener(OnGameWon);
    }

    private void ShowLevelCompleteUI(float elapsedTime, bool showNextButton)
    {
        levelCompleteScreen.SetActive(true);
        nextButton.gameObject.SetActive(showNextButton);
        ResetAllScales();

        UpdateTimeText(elapsedTime);

        int starsEarned = CalculateStars(elapsedTime);
        UpdateStarsUI(starsEarned);

        DOTween.Sequence()
            .Append(levelCompleteScreen.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
            .AppendInterval(0.2f)
            .AppendCallback(() => AnimateStars(starsEarned))
            .AppendInterval(0.8f)
            .AppendCallback(AnimateButtons)
            .Play();

        buttonEventSystemHandler.SetFirstSlected();

    }

    public void OnLevelComplete(float elapsedTime)
    {
        ShowLevelCompleteUI(elapsedTime, true);
    }

    public void OnGameWon(float elapsedTime)
    {
        ShowLevelCompleteUI(elapsedTime, false);
    }

    private void ResetAllScales()
    {
        levelCompleteScreen.transform.localScale = Vector3.zero;
        nextButton.transform.localScale = Vector3.zero;
        restartButton.transform.localScale = Vector3.zero;
        levelSelectButton.transform.localScale = Vector3.zero;
    }

    private int CalculateStars(float elapsedTime)
    {
        if (elapsedTime <= timeThresholds[0]) return 3;
        if (elapsedTime <= timeThresholds[1]) return 2;
        return 1;
    }

    private void UpdateStarsUI(int starsEarned)
    {
        ClearExistingStars();

        animatedStars = new Image[starsEarned];

        for (int i = 0; i < starsEarned; i++)
        {
            Image starImage = ObjectPool.DequeueObject<Image>(STAR_POOL_KEY);
            GameObject starInstance = starImage.gameObject;

            starInstance.transform.SetParent(starImagesPosition[i].transform);
            starInstance.transform.localPosition = Vector3.zero;
            starInstance.transform.localScale = Vector3.zero;
            starInstance.SetActive(true);

            currentStarInstances.Add(starInstance);

            animatedStars[i] = starImage;
        }
    }

    private void ClearExistingStars()
    {
        foreach (GameObject starInstance in currentStarInstances)
        {
            if (starInstance != null)
            {
                Image starImage = starInstance.GetComponent<Image>();
                ObjectPool.EnqueueObject(starImage, STAR_POOL_KEY);
            }
        }

        currentStarInstances.Clear();
        animatedStars = null;
    }

    private void AnimateStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (animatedStars != null && i < animatedStars.Length && animatedStars[i] != null)
            {
                animatedStars[i].transform
                    .DOScale(Vector3.one, 0.6f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(i * 0.2f);
            }
        }
    }

    private void AnimateButtons()
    {
        nextButton.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
        restartButton.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
        levelSelectButton.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
    }

    private void UpdateTimeText(float elapsedTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);

        textTime.text = $"<mspace=0.5em>Time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}";
    }

    // Optional: Clean up when this object is destroyed
    private void OnDestroy()
    {
        ClearExistingStars();
    }
}
