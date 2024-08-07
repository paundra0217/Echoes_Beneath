using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace RDCT.Menu.SettingsMenu
{
    public class SettingsAudio : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] AudioMixer mixer;
        [SerializeField] Slider sliderMaster;
        [SerializeField] Slider sliderBGM;
        [SerializeField] Slider sliderSFX;
        [SerializeField] TMP_Dropdown inputDeviceDropdown;

        private string previousInputDevice;

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

            List<TMP_Dropdown.OptionData> inputDeviceList = new List<TMP_Dropdown.OptionData>();
            inputDeviceList.Add(new TMP_Dropdown.OptionData("Built-in Microphone"));
            foreach (var mic in Microphone.devices)
            {
                inputDeviceList.Add(new TMP_Dropdown.OptionData(mic));
            }
            inputDeviceDropdown.options = inputDeviceList;

            var selectedInputDevice = 0;
            for (int i = 0; i < inputDeviceList.Count; i++)
            {
                if (inputDeviceDropdown.options[i].text == settings.inputDevice) 
                {
                    selectedInputDevice = i;
                    break;
                }
            }
            inputDeviceDropdown.value = selectedInputDevice;

            previousInputDevice = settings.inputDevice;
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

        public void SetInputDevice(int value)
        {
            if (value != 0)
                Settings.Instance.GetUserSettings().inputDevice = inputDeviceDropdown.options[inputDeviceDropdown.value].text;
            else
                Settings.Instance.GetUserSettings().inputDevice = "";
        }

        public void DiscardSettings()
        {
            Settings.Instance.GetUserSettings().inputDevice = previousInputDevice;
        }

        public void SaveSettings(SOSettings settings)
        {
            settings.masterVolume = sliderMaster.value;
            settings.BGMVolume = sliderBGM.value;
            settings.SFXVolume = sliderSFX.value;

            if (inputDeviceDropdown.value == 0)
                settings.inputDevice = "";
            else
                settings.inputDevice = inputDeviceDropdown.options[inputDeviceDropdown.value].text;
        }
    }
}
