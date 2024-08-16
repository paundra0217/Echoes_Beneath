using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class dialogBase : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public dialog[] dialogs;

    private void Awake()
    {
        foreach (var dialog in dialogs)
        {
            dialog.dialogGUI = textBox;
        }
    }

    private void Start()
    {
        textBox.gameObject.SetActive(false);
    }

    public void panggilDialog(string titleDial)
    {
        dialog titledialogs = Array.Find<dialog>(dialogs, s => s.title == titleDial);

        if (titledialogs != null)
        {
            textBox.gameObject.SetActive (true);

            if (titledialogs.speaker.Length > 0)
                titledialogs.dialogGUI.text = titledialogs.speaker + " : \"" + titledialogs.dialogs + "\"";
            else
                titledialogs.dialogGUI.text = "\"" + titledialogs.dialogs + "\"";

            StartCoroutine(waktuTunggu(titledialogs, titledialogs.dialogs.Length / 10, titledialogs));
        }
        else return;
    }

    IEnumerator waktuTunggu(dialog ini, float tungguLama, dialog dialo)
    {
        while (true)
        {
            yield return new WaitForSeconds(tungguLama);
            hentikanDialog(ini);
            dialo.dialogGUI.gameObject.SetActive(false);
            break;
        }
    }

    public void hentikanDialog(dialog ini)
    {
        ini.dialogGUI.text = "";
    }
}

[Serializable]
public class dialog
{
    public string title;
    public string speaker;
    [TextArea] public string dialogs;

    [HideInInspector] public TextMeshProUGUI dialogGUI;
}
