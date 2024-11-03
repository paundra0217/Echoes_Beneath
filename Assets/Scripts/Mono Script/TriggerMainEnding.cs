using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMainEnding : MonoBehaviour
{
    dialogBase dialogBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {

            if (FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().GetIndex() == 4)
            {
                GotoMainEnding();
            }
            else
            {
                FindAnyObjectByType<dialogBase>().GetComponent<dialogBase>().panggilDialog("2.20");
                //dialogBase.panggilDialog("2.6");
            }

        }
    }

    public void GotoMainEnding()
    {
        SceneManager.LoadScene("EscapeEnding");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
