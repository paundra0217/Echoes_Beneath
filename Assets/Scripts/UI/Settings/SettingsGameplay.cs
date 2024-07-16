using System;
using UnityEngine;
using UnityEngine.UI;

namespace RDCT.Menu.SettingsMenu
{
    public class SettingsGameplay : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] PlayerStats stats;
        [SerializeField] Slider sliderSensitivity;

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
        }

        // String.Format("{0:#0.0}", sliderSensitivity.value);
        public void SetSensitivity()
        {
            stats.lookSpeed = (float)Math.Round(sliderSensitivity.value, 2);
        }

        public void SaveSettings(SOSettings settings)
        {
            settings.sensitivity = (float)Math.Round(sliderSensitivity.value, 2);
        }
    }

}