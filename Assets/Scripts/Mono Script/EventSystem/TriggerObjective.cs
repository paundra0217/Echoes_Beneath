using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType {Clear, Spawn }

public class TriggerObjective : MonoBehaviour
{
    [SerializeField] private string ObjectiveName;
    [SerializeField] private TriggerType triggerType;
    private bool Triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (Triggered) return;

        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            Triggered = true;
            Debug.Log("Oke");
            if(triggerType == TriggerType.Clear)
            {
                FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().ObjectiveClear(ObjectiveName);
            }
            else
            {
                FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().TriggerObjective(ObjectiveName);
            }


        }
    }

}
