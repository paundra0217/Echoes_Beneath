using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RDCT.PlayerController;

public class PuzzleInteract : InteractObject
{
    public CinemachineVirtualCamera virtualCamera;
    private PuzzleBase puzzle;
    public bool minigameskelar = false;
    private void Start()
    {
        puzzle = GetComponent<PuzzleBase>();
    }

    public override void Interaction()
    {
        if (minigameskelar)
        {
            return;
        }
        virtualCamera.gameObject.SetActive(true);
        FindObjectOfType<InputManager>().GetComponent<InputManager>().enabled = false;
        puzzle.enabled = true;

    }

}
