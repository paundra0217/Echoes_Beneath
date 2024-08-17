using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLari : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            PlayerMotor playerMotor = other.GetComponent<PlayerMotor>();
            playerMotor.CanRun = true;
        }
    }

}
