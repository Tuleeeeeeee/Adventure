using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer musicMixer;

    private VisualElement _container;

    Slider _slider;
    
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _container = root.Q<VisualElement>("container");
        _slider = root.Q<Slider>("Music_Slider");

        
    }
    private void Update()
    {
        Debug.Log(_slider.value);
        setMusicVolume();
    }

    public void setMusicVolume()
    {
        musicMixer.SetFloat("music", _slider.value);
    }

}
