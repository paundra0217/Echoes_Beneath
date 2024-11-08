using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GeneralTrigger : MonoBehaviour
{
    public UnityEvent Activate;
    public UnityEvent Deactivate;
    void OnTriggerEnter (Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(UnityEngine.Random.Range(1,8) == 7)
            {
                Activate.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        Deactivate.Invoke();
    }
}
