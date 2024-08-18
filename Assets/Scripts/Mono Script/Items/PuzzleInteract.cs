using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using RDCT.PlayerController;

public class PuzzleInteract : InteractObject
{
    public CinemachineVirtualCamera virtualCamera;
    private PuzzleBase puzzle;
    public bool minigameskelar = false;
    public GameObject TutorText;
    [SerializeField] bool ButuhMouse;
    private void Start()
    {
        puzzle = GetComponent<PuzzleBase>();
    }

    public override void Interaction()
    {
        if (minigameskelar)
        {
            //gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            return;
        }
        if (ButuhMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        TutorText.SetActive(true);
        virtualCamera.gameObject.SetActive(true);
        FindObjectOfType<PlayerMotor>().GetComponent<PlayerMotor>().ChangeState(true);
        FindObjectOfType<InputManager>().GetComponent<InputManager>().enabled = false;
        puzzle.enabled = true;

    }

}
