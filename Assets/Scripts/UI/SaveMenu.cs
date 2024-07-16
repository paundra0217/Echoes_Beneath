using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RDCT.Menu
{
    public class SaveMenu : MonoBehaviour, IMenuWindow
    {
        [SerializeField] private GameObject[] saveSlots;
        [SerializeField] private GameObject autoSaveSlot;

        private CanvasGroup cg;

        private static SaveMenu _instance;
        public static SaveMenu Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("SaveMenu is null");

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

            FetchSavedGames();
        }
            
        private void FetchSavedGames()
        {
            // load saved games here
            Button loadButton = autoSaveSlot.transform.Find("Actions/BtnSaveContinue").GetComponent<Button>();
            TMP_Text detailText = autoSaveSlot.transform.Find("SaveDetail").GetComponent<TMP_Text>();

            loadButton.enabled = false;
            detailText.text = "<Empty>";

            foreach (var slot in saveSlots)
            {
                loadButton = slot.transform.Find("Actions/BtnSaveNewGame").GetComponent<Button>();
                detailText = slot.transform.Find("SaveDetail").GetComponent<TMP_Text>();
                Button deleteButton = slot.transform.Find("Actions/BtnSaveDelete").GetComponent<Button>();

                //loadButton.enabled = false;
                deleteButton.gameObject.SetActive(false);
                detailText.text = "50% - " + DateTime.Now.ToString("dd MMMM yyyy HH:mm");
            }
        }

        //private void PlayNewGame(int slotID)
        //{

        //}

        public void LoadGame(int slotID)
        {
            Debug.LogFormat("Loading slot {0}", slotID);
        }

        public void DeleteSave(int slotID)
        {
            Debug.LogFormat("Deleting slot {0}", slotID);
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
