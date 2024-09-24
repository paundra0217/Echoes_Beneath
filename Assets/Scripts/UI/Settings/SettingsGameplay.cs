using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RDCT.Menu.SettingsMenu
{
    public class SettingsGameplay : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] PlayerStats stats;
        [SerializeField] Slider sliderSensitivity;
        [SerializeField] TMP_InputField inputSensitivity;

        private static SettingsGameplay _instance;
        public static SettingsGameplay Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Settings is null");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void InitializeSettings(SOSettings settings)
        {
            sliderSensitivity.value = settings.sensitivity;
            inputSensitivity.text = Math.Round(sliderSensitivity.value, 2).ToString();
        }

        public void LoadSettings(SOSettings settings)
        {
            stats.lookSpeed = (float)Math.Round(settings.sensitivity, 2);
        }

        // String.Format("{0:#0.0}", sliderSensitivity.value);
        public void SetSensitivity()
        {
            inputSensitivity.text = Math.Round(sliderSensitivity.value, 2).ToString();
            stats.lookSpeed = (float)Math.Round(sliderSensitivity.value, 2);
        }

        public void ValidateSensitivityInput()
        {
            float newSensitivity;

            bool isValid = float.TryParse(inputSensitivity.text, out newSensitivity);
            bool isOutOfRange = newSensitivity > sliderSensitivity.maxValue || newSensitivity < sliderSensitivity.minValue;

            if (isValid && !isOutOfRange)
                sliderSensitivity.value = newSensitivity;
            else
                sliderSensitivity.value = stats.lookSpeed;

            SetSensitivity();
        }

        public void SaveSettings(SOSettings settings)
        {
            settings.sensitivity = (float)Math.Round(sliderSensitivity.value, 2);
        }
    }

}