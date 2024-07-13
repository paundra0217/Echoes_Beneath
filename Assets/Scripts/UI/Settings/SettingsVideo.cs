using TMPro;
using UnityEngine;

namespace RDCT.Menu
{
    public class SettingsVideo : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private TMP_Dropdown displayMode;
        [SerializeField] private TMP_Dropdown resolution;

        private static SettingsVideo _instance;
        public static SettingsVideo Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("SettingsVideo is null");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void InitializeSettings(SOSettings settings)
        {
            displayMode.value = settings.displayMode;
            resolution.value = settings.resolution;
        }

        public void ChangeDisplayMode(int value)
        {

            switch (value)
            {
                case 0: // Fullscreen
                    if (Application.platform == RuntimePlatform.WindowsPlayer)
                        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    else
                        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;

                case 1: // Windowed Fullscreen
                    if (Application.platform == RuntimePlatform.OSXPlayer)
                        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                    else
                        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;

                case 2: // Windowed
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }
        }

        public void ChangeResolution(int value)
        {
            switch (value)
            {
                case 0: // 1280x720
                    Screen.SetResolution(1280, 720, Screen.fullScreenMode);
                    break;

                case 1: // 1366x768
                    Screen.SetResolution(1366, 768, Screen.fullScreenMode);
                    break;

                case 2: // 1600x900
                    Screen.SetResolution(1600, 900, Screen.fullScreenMode);
                    break;

                case 3: // 1920x1080
                    Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
                    break;
            }
        }

        public void SaveSettings(SOSettings settings)
        {
            settings.displayMode = displayMode.value;
            settings.resolution = resolution.value;
        }
    }
}
