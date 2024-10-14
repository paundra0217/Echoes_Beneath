using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObjectiveTrigger : dialogBase
{
    [SerializeField] GameObject objectiveUI;
    [SerializeField] GameObject objectiveTitle;
    public override void OnDialogEndEvent()
    {
        objectiveUI.SetActive(true);
        objectiveTitle.SetActive(true);
    }

}
