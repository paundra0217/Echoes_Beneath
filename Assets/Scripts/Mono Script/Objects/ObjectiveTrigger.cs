using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectiveUI;
    [SerializeField] GameObject objectiveTitle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            objectiveUI.SetActive(true);
            objectiveTitle.SetActive(true);
        }
    }

}
