using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action FlickeringLight;
    TriggerChecker triggerChecker;
    bool trigger;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        trigger = GetComponent<TriggerChecker>().trigger;
    }
}
