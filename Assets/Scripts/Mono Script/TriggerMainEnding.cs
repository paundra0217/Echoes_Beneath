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

            if (FindAnyObjectByType<FuseBox>().GetComponent<FuseBox>().GetFuse())
            {
                SceneManager.LoadScene("EscapeEnding");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                dialogBase.panggilDialog("2.6");
            }

        }
    }




}
