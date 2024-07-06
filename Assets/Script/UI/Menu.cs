using UnityEngine;

namespace RDCT.MainMenu
{
    public class Menu : MonoBehaviour
    {
        private static CanvasGroup cg;

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public static void PlayGame()
        {
            //lets goooooo!!!
        } 

        public void OpenSettings()
        {
            CloseMainMenu();

            Settings.Instance.InitializeSettings();
        }

        public static void ExitGame()
        {
            Application.Quit();
        }

        private static void CloseMainMenu()
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
            cg.interactable = false;
        }

        public static void ReOpenMainMenu()
        {
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            cg.interactable = true;
        }
    }
}