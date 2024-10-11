using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("3");
    }
}
