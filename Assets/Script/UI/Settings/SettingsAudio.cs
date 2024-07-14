using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsAudio : MonoBehaviour, ISettingsMenu
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider sliderMaster;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;

    private static SettingsAudio _instance;
    public static SettingsAudio Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Settings is null");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void SetMasterVolume()
    {
        mixer.SetFloat("VolumeMaster", sliderMaster.value);
    }

    public void SetBGMVolume()
    {
        mixer.SetFloat("VolumeBGM", sliderBGM.value);
    }

    public void SetSFXVolume()
    {
        mixer.SetFloat("VolumeSFX", sliderSFX.value);
        mixer.SetFloat("VolumeVCL", sliderSFX.value);
    }

    public void InitializeSettings()
    {
        sliderMaster.value = 0;
        sliderBGM.value = 0;
        sliderSFX.value = 0;
    }

    public void SaveSettings()
    {
        
    }
}
