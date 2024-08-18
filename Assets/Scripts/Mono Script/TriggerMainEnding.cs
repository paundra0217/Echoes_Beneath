using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMainEnding : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            SceneManager.LoadScene("Escape Ending");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
