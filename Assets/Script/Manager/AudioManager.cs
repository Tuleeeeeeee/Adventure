using UnityEngine;
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource audioPlayer;
    public void PlaySound(AudioClip soundToPlay)
    {
        // example 1
        audioPlayer.clip = soundToPlay;
        audioPlayer.Play();

        // example 2

        // audioPlayer.PlayOneShot(soundToPlay);
    }
}

