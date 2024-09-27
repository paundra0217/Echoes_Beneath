using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseItem : PickUpObject
{
    public override void Interaction()
    {
        FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().PickFuseObjectiveClear();
        base.Interaction();
    }
}
