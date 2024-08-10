using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.PlayerController;

public class GeneratorPuzzle : PuzzleBase
{
    [Header("Kunci Jawaban")]
    [SerializeField] private bool Kunci1;
    [SerializeField] private bool Kunci2;
    [SerializeField] private bool Kunci3;
    [SerializeField] private bool Kunci4;
    [Header("Lever SetUp")]
    [SerializeField] private Lever Lever1;
    [SerializeField] private Lever Lever2;
    [SerializeField] private Lever Lever3;
    [SerializeField] private Lever Lever4;
    public PuzzleInteract puzzleInteract;
    public bool Benar;

    private void Start()
    {
        puzzleInteract = GetComponent<PuzzleInteract>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelMinigames();
        }

        if (Kunci1 == Lever1.Kunci)
        {
            if (Kunci2 == Lever2.Kunci)
            {
                if (Kunci3 == Lever3.Kunci)
                {
                    if (Kunci4 == Lever4.Kunci)
                    {
                        Benar = true;
                        return;
                    }
                }
            }
        }
        Benar = false;
    }

    public void WinGames()
    {
        Debug.Log("menang");
        Lever1.enabled = false;
        Lever2.enabled = false;
        Lever3.enabled = false;
        Lever4.enabled = false;
        CancelMinigames();
    }

    public void Losegames()
    {
        Debug.Log("kalah");
        CancelMinigames();
    }

    public void CancelMinigames()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        puzzleInteract.virtualCamera.gameObject.SetActive(false);
        FindObjectOfType<InputManager>().GetComponent<InputManager>().enabled = true;        

    }


}
