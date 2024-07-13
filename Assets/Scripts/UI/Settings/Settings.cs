using UnityEngine;
using UnityEngine.UI;

namespace RDCT.Menu
{
    [System.Serializable]
    enum SettingsCategory
    {
        VIDEO,
        AUDIO,
        GRAPHICS
    }

    [System.Serializable]
    class SettingsWindow
    {
        public Button windowButton;
        public GameObject windowObject;
        public SettingsCategory windowCategory;
    }

    public class Settings : MonoBehaviour, IMenuWindow
    {
        [SerializeField] private SettingsWindow[] windows;
        [SerializeField] private SOSettings defaultSettings;
        [SerializeField] private SOSettings userSettings;

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
            _instance = this;

            cg = GetComponent<CanvasGroup>();
        }

        public void OpenWindow()
        {
            SettingsVideo.Instance.InitializeSettings(userSettings);
            SettingsAudio.Instance.InitializeSettings(userSettings);
            SettingsGraphics.Instance.InitializeSettings(userSettings);
            
            cg.alpha = 1.0f;
            cg.blocksRaycasts = true;
            cg.interactable = true;

            SwitchOptions(SettingsCategory.VIDEO);
        }

        private void SwitchOptions(SettingsCategory category)
        {
            currentCategory = category;

            foreach (var window in windows)
            {
                var isEnabled = window.windowCategory == currentCategory;
                var wcg = window.windowObject.GetComponent<CanvasGroup>();

                window.windowButton.enabled = !isEnabled;

                wcg.alpha = isEnabled ? 1.0f : 0f;
                wcg.blocksRaycasts = isEnabled;
                wcg.interactable = isEnabled;
            }
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

            print(JsonUtility.ToJson(userSettings));

            CloseWindow();
        }

        public void ResetSettings()
        {
            SettingsGraphics.Instance.InitializeSettings(defaultSettings);
        }

        public void CloseWindow()
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
            cg.interactable = false;

            MainMenu.ReOpenMainMenu();
        }
    }
}
