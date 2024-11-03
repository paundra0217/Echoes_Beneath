using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RDCT.PlayerController;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMotor>())
        {
            FindAnyObjectByType<InputManager>().GetComponent<InputManager>().SetEnable(false);
            playableDirector.Play();
        }
    }

}
