using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerMainEnding : InteractObject
{

    public override void Interaction()
    {
        if (FindObjectOfType<ObjectiveManager>().GetComponent<ObjectiveManager>().CheckObjective() == false)
        {
            Debug.Log("Richard monyet");
            return;
        }

        SceneManager.LoadScene("EscapeEnding");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
