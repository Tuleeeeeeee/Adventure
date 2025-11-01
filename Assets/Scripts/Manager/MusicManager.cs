using System.Collections;
using UnityEngine;

public class MusicManager : SingletonMonoBehaviour<MusicManager>
{

    private AudioSource musicAudioSource;
    private AudioClip currentAudioClip = null;
    private Coroutine fadeOutMusicCoroutine;
    private Coroutine fadeInMusicCoroutine;

    [SerializeField] private BoolGameEventSO volumeToggleEvent;


    private bool isMuted = false;

    public override void Init()
    {
        base.Init();
        musicAudioSource = GetComponent<AudioSource>();
        if (musicAudioSource == null)
        {
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        volumeToggleEvent.RegisterListener(ToggleMute);
    }

    void OnDisable()
    {
        volumeToggleEvent.UnregisterListener(ToggleMute);
    }

    public void PlayMusic(MusicTrackSO musicTrack, float fadeOutTime = Settings.musicFadeOutTime, float fadeInTime = Settings.musicFadeInTime)
    {
        StartCoroutine(PlayMusicRoutine(musicTrack, fadeOutTime, fadeInTime));
    }

    private IEnumerator PlayMusicRoutine(MusicTrackSO musicTrack, float fadeOutTime, float fadeInTime)
    {
        if (fadeOutMusicCoroutine != null)
        {
            StopCoroutine(fadeOutMusicCoroutine);
        }
        if (fadeInMusicCoroutine != null)
        {
            StopCoroutine(fadeInMusicCoroutine);
        }
        if (musicTrack.musicClip != currentAudioClip)
        {
            currentAudioClip = musicTrack.musicClip;
            yield return fadeOutMusicCoroutine = StartCoroutine(FadeOutMusic(fadeOutTime));

            yield return fadeInMusicCoroutine = StartCoroutine(FadeInMusic(musicTrack, fadeInTime));
        }

        yield return null;
    }

    private IEnumerator FadeOutMusic(float fadeOutTime)
    {
        GameResources.Instance.musicLowSnapshot.TransitionTo(fadeOutTime);

        yield return new WaitForSeconds(fadeOutTime);
    }

    private IEnumerator FadeInMusic(MusicTrackSO musicTrack, float fadeInTime)
    {
        musicAudioSource.clip = musicTrack.musicClip;
        musicAudioSource.volume = musicTrack.musicVolume;
        musicAudioSource.Play();

        GameResources.Instance.musicOnFullSnapshot.TransitionTo(fadeInTime);

        yield return new WaitForSeconds(fadeInTime);
    }

    public void ToggleMute(bool muted)
    {
        musicAudioSource.mute = muted;
        isMuted = muted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        musicAudioSource.mute = isMuted;
    }

}