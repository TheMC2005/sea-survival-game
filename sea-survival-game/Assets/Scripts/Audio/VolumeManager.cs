using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider masterSlider;

    private void Start()
    {
        SetMusicVolume();
        SetMasterVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 50);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 50);
    }

    public void LoadData(GameData data)
    {
        masterSlider.value = data.masterSliderValue;
        musicSlider.value = data.musicSliderValue;
    }

    public void SaveData(GameData data)
    {
        data.masterSliderValue = masterSlider.value;
        data.musicSliderValue = musicSlider.value;
    }
}

