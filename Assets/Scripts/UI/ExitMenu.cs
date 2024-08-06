using UnityEngine;

namespace RDCT.Menu
{
    public class ExitMenu : MonoBehaviour, IMenuWindow
    {
        private CanvasGroup cg;

        private static ExitMenu _instance;
        public static ExitMenu Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("ExitMenu is null");

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;

            cg = GetComponent<CanvasGroup>();
        }

        public void OpenWindow()
        {
            cg.alpha = 1.0f;
            cg.blocksRaycasts = true;
            cg.interactable = true;
        }

        public void ExitGame()
        {
            Application.Quit();
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