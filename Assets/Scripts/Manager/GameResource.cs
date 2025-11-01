using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable Objects/Resources/Game Resources")]
public class GameResources : ScriptableObject
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<GameResources>("GameResources");
            return instance;
        }
    }

    #region Header PLAYER
    [Space(10)]
    [Header("PLAYER")]
    #endregion
    #region Tooltip
    [Tooltip("The current player SO, used to reference the current player between scenes")]
    #endregion
    public CurrentPlayerSO currentPlayerSO;
    public List<PlayerDetailsSO> playerDetailsList;

    #region Header MUSIC
    [Space(10)]
    [Header("MUSIC")]
    #endregion
    public AudioMixerGroup musicMasterMixerGroup;
    public MusicTrackSO mainMenuMusic;
    public AudioMixerSnapshot musicOnFullSnapshot;
    public AudioMixerSnapshot musicLowSnapshot;
    public AudioMixerSnapshot musicOffSnapshot;
}

