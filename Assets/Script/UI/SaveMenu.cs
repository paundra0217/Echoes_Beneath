using RDCT.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour, IMenuWindow
{
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

        GetSavedGames();
    }

    private void GetSavedGames()
    {
        // load saved games here
    }

    public void CloseWindow()
    {
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        cg.interactable = false;

        MainMenu.ReOpenMainMenu();
    }
}
