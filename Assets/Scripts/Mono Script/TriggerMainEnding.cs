using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMainEnding : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ini gk tau");
        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            if (FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>().CheckObjective())
            {
                SceneManager.LoadScene("EscapeEnding");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            Debug.Log("ini player");
        }
    }
}
