using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : InteractObject
{

    public override void Interaction()
    {
        if (FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().GetPickUpNoteFirstTime() == false)
        {
            FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().PickUpNoteFirstTime();
        }

        FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().ObjectiveClear("Journal");
        FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().JournalPick();
        Destroy(gameObject);
    }

}
