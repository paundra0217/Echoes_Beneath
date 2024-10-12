using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : PickUpObject
{
    public override void Interaction()
    {
        FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().ObjectiveClear("Keycard");
        base.Interaction();
    }

}
