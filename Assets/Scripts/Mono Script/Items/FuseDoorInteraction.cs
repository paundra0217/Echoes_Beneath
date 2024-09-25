using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseDoorInteraction : InteractObject
{
    public GameObject doorToOpen;

    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = doorToOpen.GetComponent<Animator>();
    }

    public void openDoor()
    {

    }
}