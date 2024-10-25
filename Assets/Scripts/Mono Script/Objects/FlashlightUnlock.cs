using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightUnlock : InteractObject
{
    public override void Interaction()
    {
        FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().CanFlashlight = true;
        Destroy(gameObject);
    }

}
