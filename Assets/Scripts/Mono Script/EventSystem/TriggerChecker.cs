using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    public UnityEvent headPopOut;
    public UnityEvent headDisappear;
    void OnTriggerEnter (Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            headPopOut.Invoke();
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        headDisappear.Invoke();
        Destroy(gameObject);
    }
}
