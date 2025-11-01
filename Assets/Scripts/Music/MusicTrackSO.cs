using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrack_", menuName = "Scriptable Objects/Sounds/MusicTrack")]
public class MusicTrackSO : ScriptableObject
{
    #region Header MUSIC TRACK DETAILS
    [Space(10)]
    [Header("MUSIC TRACK DETAILS")]
    #endregion
    public string musicName;
    public AudioClip musicClip;
    [Range(0, 1)]
    public float musicVolume = 1f;
}