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

    public void InitializeSettings(SOSettings settings)
    {
        sliderMaster.value = settings.masterVolume;
        sliderBGM.value = settings.BGMVolume;
        sliderSFX.value = settings.SFXVolume;
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

    public void SaveSettings(SOSettings settings)
    {
        settings.masterVolume = sliderMaster.value;
        settings.BGMVolume = sliderBGM.value;
        settings.SFXVolume = sliderSFX.value;
    }
}
