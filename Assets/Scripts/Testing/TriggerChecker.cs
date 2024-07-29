using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public bool trigger = false;
    void OnTriggerEnter (Collider coll)
    {
        trigger = true;
    }
}
