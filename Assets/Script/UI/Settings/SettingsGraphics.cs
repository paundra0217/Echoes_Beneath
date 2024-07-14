using RDCT.Menu;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsGraphics : MonoBehaviour, ISettingsMenu
{
    [SerializeField] private TMP_Dropdown quality;
    [SerializeField] private TMP_Dropdown antiAliasing;
    [SerializeField] private TMP_Dropdown SSO;
    [SerializeField] private TMP_Dropdown PostProcessing;
    [SerializeField] private TMP_Dropdown maxFPS;

    private static SettingsGraphics _instance;
    public static SettingsGraphics Instance
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

    public void InitializeSettings()
    {
        
    }

    public void SetQuality(int value)
    {

    }

    public void SetAA(int value)
    {

    }

    public void SetSSO(int value)
    {

    }

    public void SetPostProcessing(int value)
    {

    }

    public void SetMaxFPS(int value)
    {
        switch (value)
        {
            case 0: // uncapped
                Application.targetFrameRate = -1;
                break;

            case 1: // 30fps
                Application.targetFrameRate = 30;
                break;

            case 2: // 60fps
                Application.targetFrameRate = 60;
                break;

            case 3: // 90fps
                Application.targetFrameRate = 90;
                break;

            case 4: // 120fps
                Application.targetFrameRate = 120;
                break;
        }
    }
    
    public void SaveSettings()
    {
        
    }
}
