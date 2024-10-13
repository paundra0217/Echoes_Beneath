using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : InteractObject
{

    public override void Interaction()
    {
        FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().ObjectiveClear("Journal");
        FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().JournalPick();
        Destroy(gameObject);
    }

}
