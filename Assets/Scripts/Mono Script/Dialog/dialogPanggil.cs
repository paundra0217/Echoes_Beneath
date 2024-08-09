using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogPanggil : MonoBehaviour
{
    public dialogBase dialogBase;

    public string dialogTitle;

    private void Start()
    {
        dialogBase = GameObject.FindAnyObjectByType<dialogBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogBase.panggilDialog(dialogTitle);
    }
}
