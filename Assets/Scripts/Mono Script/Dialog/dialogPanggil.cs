using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogPanggil : MonoBehaviour
{
    public dialogBase dialogBase;

    public string dialogTitle;
    public bool called;

    private void Start()
    {
        dialogBase = GameObject.FindAnyObjectByType<dialogBase>();
        called = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (called) return;
        DialogCalled();
    }

    public virtual void DialogCalled()
    {
        dialogBase.panggilDialog(dialogTitle);
        called = true;
    }
}
