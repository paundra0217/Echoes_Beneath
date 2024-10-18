using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTrueEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {

            if (FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().GetIndex() == 5)
            {
                SceneManager.LoadScene("EscapeEnding");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                FindAnyObjectByType<dialogBase>().GetComponent<dialogBase>().panggilDialog("2.6");
                //dialogBase.panggilDialog("2.6");
            }

        }
    }
}
