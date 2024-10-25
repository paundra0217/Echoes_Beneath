using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPanggilBanyak : dialogPanggil
{
    [SerializeField] string[] DialogTitle;

    public override void DialogCalled()
    {
        foreach(string oke in DialogTitle)
        {
            dialogBase.panggilDialog(oke);
        }
        //base.DialogCalled();
    }
}
