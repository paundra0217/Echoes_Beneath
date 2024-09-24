using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerJokeEnding : InteractObject
{

    public override void Interaction()
    {
        SceneManager.LoadScene("JokeEnding");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

}
