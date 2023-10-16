using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        SetMusicVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
}
