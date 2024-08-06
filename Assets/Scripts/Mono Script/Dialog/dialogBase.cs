using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    public void panggilDialog(string titleDial)
    {
        dialog titledialogs = Array.Find<dialog>(dialogs, s => s.title == titleDial);

        if (titledialogs != null)
        {
            titledialogs.dialogGUI.text = titledialogs.dialogs;
            StartCoroutine(waktuTunggu(titledialogs, titledialogs.dialogs.Length / 3));
        }
        else return;
    }

    IEnumerator waktuTunggu(dialog ini, float tungguLama)
    {
        while (true)
        {
            yield return new WaitForSeconds(tungguLama);
            hentikanDialog(ini);
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
