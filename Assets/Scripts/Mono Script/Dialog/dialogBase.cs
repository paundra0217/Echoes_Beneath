using JetBrains.Annotations;
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
    private List<dialog> dialogQ = new List<dialog>();

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

        if (titledialogs != null && titledialogs.canBeCalled)
        {
            if (!dialogQ.Contains(titledialogs)) dialogQ.Add(titledialogs);

            if (!textBox.isActiveAndEnabled)
                textBox.gameObject.SetActive (true);

            if (dialogQ[0].speaker.Length > 0)
                dialogQ[0].dialogGUI.text = dialogQ[0].speaker + " : \"" + dialogQ[0].dialogs + "\"";
            else
                dialogQ[0].dialogGUI.text = "\"" + dialogQ[0].dialogs + "\"";

            var waktu = 0;

            if (dialogQ[0].dialogs.Length < 40) waktu = 8;
            else if (dialogQ[0].dialogs.Length > 60) waktu = 10;

            StartCoroutine(waktuTunggu(dialogQ[0], waktu));
        }
        else return;
    }

    IEnumerator waktuTunggu(dialog ini, float tungguLama)
    {
        while (true)
        {
            yield return new WaitForSeconds(tungguLama);
            if (dialogQ.Count > 0)
            {
                dialogQ.Remove(dialogQ[0]);
                if (dialogQ.Count != 0)
                    panggilDialog(dialogQ[0].title);
                else
                {
                    textBox.gameObject.SetActive(false);
                    break;
                }
            }
            else
            {
                //hentikanDialog(ini);
                textBox.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void hentikanDialog(dialog ini)
    {
        dialogQ.Remove(ini);
        ini.dialogGUI.text = "";
    }

    public void dialogConditionFull(string titleDial)
    {
        dialog titledialogs = Array.Find<dialog>(dialogs, s => s.title == titleDial);

        if (titledialogs != null)
        {
            titledialogs.canBeCalled = true;
        }
    }
}

[Serializable]
public class dialog
{
    public string title;
    public string speaker;
    [TextArea] public string dialogs;
    public bool canBeCalled = true;

    [HideInInspector] public TextMeshProUGUI dialogGUI;
}
