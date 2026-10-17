using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private GameObject levelSelectScreen;


    [Header("Events")]
    [SerializeField] private GameEventSO levelSelectEvent;


    void OnEnable()
    {
        levelSelectEvent.RegisterListener(ActiveLevelSelectScreen);
    }

    void OnDisable()
    {
        levelSelectEvent.UnregisterListener(ActiveLevelSelectScreen);
    }

    public void ActiveLevelSelectScreen()
    {
        levelSelectScreen.SetActive(true);
    }
}