using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip[] Background;
    public AudioClip Jump;
    public AudioClip Hit;


    [SerializeField]
    private AudioMixer audioMixer;

    public bool VolumeOn { get; private set; }
    public float VolumeValue { get; private set; }

    private int currentClipIndex = 0;
    void Awake()
    {
        // audioMixer = GetComponent<AudioMixer>();
      
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        StartCoroutine(PlaySongSequence());
     
        if(VolumeOn)
        {

        }
        SetVolume(VolumeValue);
    }
    private void Update()
    {
        PlayerPrefs.SetInt("VolumeOn", VolumeOn ? 1 : 0);
        PlayerPrefs.SetFloat("SoundVolume", VolumeValue);
    }
    IEnumerator PlaySongSequence()
    {
        // Play the current song
        musicSource.clip = Background[currentClipIndex];
        Debug.Log(musicSource.clip.name);
        musicSource.Play();
        yield return new WaitForSeconds(Background[currentClipIndex].length);
        currentClipIndex++;
        if (currentClipIndex >= Background.Length)
        {
            currentClipIndex = 0; // Loop back to the beginning
        }
        StartCoroutine(PlaySongSequence());
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public float SetVolume(float volumeValue)
    {
        VolumeValue = volumeValue;
        return VolumeValue;
    }
    public void MuteVolume()
    {
        AudioListener.volume = 0;
    }
}
