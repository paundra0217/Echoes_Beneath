using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RDCT.Menu.SettingsMenu
{
    [Serializable]
    enum SettingsCategory
    {
        GAMEPLAY,
        VIDEO,
        AUDIO,
        GRAPHICS
    }

    [Serializable]
    class SettingsWindow
    {
        public Button windowButton;
        public GameObject windowObject;
        public SettingsCategory windowCategory;
    }

    // Biar bisa diloading dari file JSON
    [Serializable]
    public class SettingsStorage
    {
        public float sensitivity;

        public int displayMode;
        public int resolution;

        public float masterVolume;
        public float BGMVolume;
        public float SFXVolume;
        public string inputDevice;

        public int quality;
        public int antiAliasing;
        public int VSync;
        public int SSO;
        public int postProcessing;
        public int maxFPS;
    }

    public class Settings : MonoBehaviour, IMenuWindow
    {
        [SerializeField] private SettingsWindow[] windows;
        [SerializeField] private SOSettings defaultSettings;
        [SerializeField] private SOSettings userSettings;
        [SerializeField] private UnityEvent functionAfterClose;
        [SerializeField] private UnityEvent fucntionAfterSave;

        private static SettingsCategory currentCategory;
        private static CanvasGroup cg;

        private static Settings _instance;
        public static Settings Instance
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
            //InitializeSettings();
            //File.WriteAllText(Application.persistentDataPath + "/DefaultSettings.json", JsonUtility.ToJson(defaultSettings));

            _instance = this;

            cg = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            LoadSettingsFromJson();
            CheckMicrophones();
        }

        private void CheckMicrophones()
        {
            bool microphoneExisted = false;

            foreach (var mic in Microphone.devices)
            {
                if (mic == userSettings.inputDevice)
                    microphoneExisted = true;
            }

            if (!microphoneExisted)
            {
                SettingsAudio.Instance.SetInputDevice(0);
            }
        }

        private void LoadSettingsFromJson()
        {
            try
            {
                var userSettingsJson = File.ReadAllText(Application.persistentDataPath + "/UserSettings.json");
                var userSettingsObject = JsonUtility.FromJson<SettingsStorage>(userSettingsJson);

                // Gameplay Settings
                userSettings.sensitivity = userSettingsObject.sensitivity != 0f ? userSettingsObject.sensitivity : defaultSettings.sensitivity;

                // Video Settings
                userSettings.displayMode = userSettingsObject.displayMode;
                userSettings.resolution = userSettingsObject.resolution;

                // Audio Settings
                userSettings.masterVolume = userSettingsObject.masterVolume;
                userSettings.BGMVolume = userSettingsObject.BGMVolume;
                userSettings.SFXVolume = userSettingsObject.SFXVolume;
                userSettings.inputDevice = userSettingsObject.inputDevice;

                // Graphics Settings
                userSettings.quality = userSettingsObject.quality;
                userSettings.antiAliasing = userSettingsObject.antiAliasing;
                userSettings.VSync = userSettingsObject.VSync;
                userSettings.SSO = userSettingsObject.SSO;
                userSettings.postProcessing = userSettingsObject.postProcessing;
                userSettings.maxFPS = userSettingsObject.maxFPS;
            }
            catch
            {
                userSettings = defaultSettings;
                File.WriteAllText(Application.persistentDataPath + "/UserSettings.json", JsonUtility.ToJson(userSettings));
            }

            SettingsGameplay.Instance.LoadSettings(userSettings);
            SettingsVideo.Instance.LoadSettings(userSettings);
            SettingsAudio.Instance.LoadSettings(userSettings);
            SettingsGraphics.Instance.LoadSettings(userSettings);
        }

        public void OpenWindow()
        {
            LoadSettingsFromJson();

            SettingsGameplay.Instance.InitializeSettings(userSettings);
            SettingsVideo.Instance.InitializeSettings(userSettings);
            SettingsAudio.Instance.InitializeSettings(userSettings);
            SettingsGraphics.Instance.InitializeSettings(userSettings);
            
            cg.alpha = 1.0f;
            cg.blocksRaycasts = true;
            cg.interactable = true;

            SwitchOptions(SettingsCategory.GAMEPLAY);
        }

        private void SwitchOptions(SettingsCategory category)
        {
            currentCategory = category;

            foreach (var window in windows)
            {
                var isEnabled = window.windowCategory == currentCategory;
                var wcg = window.windowObject.GetComponent<CanvasGroup>();

                window.windowButton.interactable = !isEnabled;

                wcg.alpha = isEnabled ? 1.0f : 0f;
                wcg.blocksRaycasts = isEnabled;
                wcg.interactable = isEnabled;
            }
        }

        public void SetCategoryToGameplay()
        {
            SwitchOptions(SettingsCategory.GAMEPLAY);
        }

        public void SetCategoryToVideo()
        {
            SwitchOptions(SettingsCategory.VIDEO);
        }

        public void SetCategoryToAudio()
        {
            SwitchOptions(SettingsCategory.AUDIO);
        }

        public void SetCategoryToGraphics()
        {
            SwitchOptions(SettingsCategory.GRAPHICS);
        }

        public void SaveSettings()
        {
            SettingsAudio.Instance.SaveSettings(userSettings);
            SettingsVideo.Instance.SaveSettings(userSettings);
            SettingsGraphics.Instance.SaveSettings(userSettings);
            SettingsGameplay.Instance.SaveSettings(userSettings);

            File.WriteAllText(Application.persistentDataPath + "/UserSettings.json", JsonUtility.ToJson(userSettings));

            fucntionAfterSave.Invoke();

            CloseWindow();
        }

        public void DiscardSettings()
        {
            SettingsAudio.Instance.DiscardSettings();

            CloseWindow();
        }

        public void ResetSettings()
        {
            SettingsGraphics.Instance.InitializeSettings(defaultSettings);
        }

        public SOSettings GetUserSettings()
        {
            return userSettings;
        }

        public void CloseWindow()
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
            cg.interactable = false;

            //MainMenu.ReOpenMainMenu();
            functionAfterClose.Invoke();
        }
    }
}
