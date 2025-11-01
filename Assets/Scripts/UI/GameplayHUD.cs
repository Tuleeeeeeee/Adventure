using System;
using TMPro;
using Tuleeeeee.Enums;
using UnityEngine;
using UnityEngine.UI;


public class GameplayHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button volumeButton;

    [Header("Events")]
    [SerializeField] private GameEventSO nextLevelEvent;
    [SerializeField] private GameEventSO prevLevelEvent;
    [SerializeField] private GameEventSO restartEvent;
    [SerializeField] private IntIntGameEventSO levelChangedEvent;
    [SerializeField] private BoolGameEventSO volumeToggleEvent;
    [SerializeField] private FloatGameEvent timeUpdateEvent;
    [SerializeField] private FloatGameEvent timeChangeEvent ;
    [SerializeField] private GameStateGameEventSO gameStateChangeEvent;

    [Header("Icons")]
    [SerializeField] private Sprite volumeOnIcon;
    [SerializeField] private Sprite volumeOffIcon;

    private bool isMuted = false;

    private void OnEnable()
    {
        nextButton.onClick.AddListener(() => nextLevelEvent.Raise());
        prevButton.onClick.AddListener(() => prevLevelEvent.Raise());
        restartButton.onClick.AddListener(() => restartEvent.Raise());
        volumeButton.onClick.AddListener(OnVolumeButtonClicked);
        levelChangedEvent.RegisterListener(UpdateButtons);
        timeChangeEvent.RegisterListener(UpdateTime);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveAllListeners();
        prevButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        volumeButton.onClick.RemoveAllListeners();
        levelChangedEvent.UnregisterListener(UpdateButtons);
        timeChangeEvent.UnregisterListener(UpdateTime);
    }

    private void OnVolumeButtonClicked()
    {
        isMuted = !isMuted;
        volumeToggleEvent.Raise(isMuted);
        SetVolumeIcon(!isMuted);
    }

    private void UpdateButtons((int currentIndex, int maxIndex) levelInfo)
    {
        prevButton.interactable = levelInfo.currentIndex > 0;
        nextButton.interactable = levelInfo.currentIndex < levelInfo.maxIndex;
    }

    public void UpdateTime(float elapsedTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        timeText.text = $"<mspace=0.5em>{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}";
    }

    private void SetVolumeIcon(bool isVolumeOn)
    {
        volumeButton.image.sprite = isVolumeOn ? volumeOnIcon : volumeOffIcon;
    }
}
