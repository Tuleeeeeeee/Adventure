using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private VisualElement container;

    private Slider volumeSlider;

    public float volumeValue;
    private float currentVolumeValue;

    private void Awake()
    {

        // _volumeSlider.value = _currentVolumeValue;
    }
    private void Start()
    {
        currentVolumeValue = PlayerPrefs.GetFloat("music", volumeValue);
        Debug.Log("_currentVolumeValue: " + currentVolumeValue);
        LoadSettingsData(currentVolumeValue);
        var root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<VisualElement>("container");
        volumeSlider = root.Q<Slider>("VolumeSlider");

        Debug.Log("mlem: " + currentVolumeValue);
        volumeSlider.value = currentVolumeValue;
        Debug.Log("defaut: " + volumeSlider.value);

        volumeSlider.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            currentVolumeValue = volumeSlider.value;
            SaveSettingsData(currentVolumeValue);
            LoadSettingsData(currentVolumeValue);
        });




        /*_volumeValue = _volumeSlider.value;
        _audioManager.SetVolume(Mathf.Log10(_volumeValue) * 20);
        Debug.Log(_volumeValue);*/


        /*   var csharpSlider = new Slider("C# Slider", 0, 100);
           csharpSlider.SetEnabled(false);
           csharpSlider.AddToClassList("some-styled-slider");
           csharpSlider.value = _volumeSlider.value;
           _container.Add(csharpSlider);

           _volumeSlider.RegisterCallback<ChangeEvent<float>>((evt) =>
           {
               _volumeSlider.value = evt.newValue;
           });*/
        /* _buttonStart.RegisterCallback<MouseEnterEvent>(delegate { _buttonStart.style.opacity = 0.8f; });
         _buttonStart.RegisterCallback<MouseLeaveEvent>(delegate { _buttonStart.style.opacity = 1f; });*/

    }
/*    private void Update()
    {
        Debug.Log("Volume" + _currentVolumeValue);
        Debug.Log("Slide" + _volumeSlider.value);
    }*/
    void LoadSettingsData(float _volumeValue)
    {
        float volume = PlayerPrefs.GetFloat("music", _volumeValue);
        audioMixer.SetFloat("music", (Mathf.Log10(volume) * 20));
    }
    void SaveSettingsData(float _volumeValue)
    {
        PlayerPrefs.SetFloat("music", _volumeValue);
    }
}
