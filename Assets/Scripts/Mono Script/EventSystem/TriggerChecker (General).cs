using System;
using System.Collections;
using System.Collections.Generic;
using RDCT.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    public UnityEvent _TriggerEnter;
    public UnityEvent _TriggerExit;
    public UnityEvent _TriggerKelar;
    private bool isActivated = false;
    [SerializeField] private string AudioName; 
    void OnTriggerEnter (Collider collider)
    {
        if(isActivated) return;
        if(collider.CompareTag("Player"))
        {
            _TriggerEnter.Invoke();
            Debug.Log("Yes");
            AudioController.Instance.PlaySFX(AudioName);
            isActivated = true;
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        _TriggerExit.Invoke();
    }

    public void TriggerMatiin()
    {
        _TriggerKelar.Invoke();
    }
}
